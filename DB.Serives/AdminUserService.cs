using DB.IService;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.Common;
using WarmHome.DTO;

namespace DB.Service
{
    public class AdminUserService : IAdminUserService
    {
        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<AdminUserEntity> serives = new BaseService<AdminUserEntity>(db);
                bool exists = serives.GetAll().Any(e => e.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("账号已存在");
                }
                else
                {
                    string salt = CommonHelper.CreateVerifyCode(4);
                    string pwd = CommonHelper.CalcMD5(password + salt);
                    AdminUserEntity admin = new AdminUserEntity() {
                        CityId = cityId,
                        Email = email,
                        Name = name,
                        PhoneNum = phoneNum,
                        LoginErrorTimes = 0,
                        PasswordSalt = salt,
                        PasswordHash = pwd
                    };
                    db.AdminUsers.Add(admin);
                    db.SaveChanges();
                    return admin.Id;
                }
            }
        }

        public IEnumerable<AdminUserDTO> GetAll(long? cityId)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<AdminUserEntity> serives = new BaseService<AdminUserEntity>(db);
                return serives.GetAll().Include(e => e.City).AsNoTracking().Select(item => DTO(item)).ToList();
            }
        }
        public AdminUserDTO DTO(AdminUserEntity entity)
        {
            string cityName;
            if (entity.City!=null)
            {
                cityName = entity.City.Name;
            }
            else
            {
                cityName = "总部";
            }
            AdminUserDTO admin = new AdminUserDTO()
            {
                CityId = entity.CityId,
                CityName = cityName,
                Name = entity.Name,
                CreateDateTime = entity.CreateDateTime,
                Email = entity.Email,
                PhoneNum = entity.PhoneNum,
                Id = entity.Id,
                LoginErrorTimes = entity.LoginErrorTimes,
                LastLoginErrorTime = entity.LastLoginErrorDateTime
            };
            return admin;
        }

        public void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<AdminUserEntity> serives = new BaseService<AdminUserEntity>(db);
                var data = serives.GetById(id);
                if (data==null)
                {
                    throw new ArgumentException("数据不存在");
                }
                if (!string.IsNullOrEmpty(password))
                {
                    data.PasswordHash = CommonHelper.CalcMD5(password + data.PasswordSalt);
                }
                data.Name = name;
                data.PhoneNum = phoneNum;
                data.Name = name;
                data.Email = email;
                data.CityId = cityId;
                db.SaveChanges();
            }
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<AdminUserEntity> serives = new BaseService<AdminUserEntity>(db);
                var data  = serives.GetAll().AsNoTracking().SingleOrDefault(e => e.PhoneNum == phoneNum);
                if (data==null)
                {
                    return false;
                }
                return data.PasswordHash == CommonHelper.CalcMD5(password + data.PasswordSalt);
            }
        }

        public IEnumerable<AdminUserDTO> GetAll()
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<AdminUserEntity> serives = new BaseService<AdminUserEntity>(db);
                return serives.GetAll().Include(e => e.City).AsNoTracking().ToList().Select(item => DTO(item));

            }
        }

        public AdminUserDTO GetById(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext() )
            {
                BaseService<AdminUserEntity> serives = new BaseService<AdminUserEntity>(db);
                var data = serives.GetAll().Include(e => e.City).AsNoTracking().SingleOrDefault(e=>e.Id==id);
                if (data==null)
                {
                    return null;
                }
                else
                {
                    return DTO(data);
                }
            }
        }

        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<AdminUserEntity> serives = new BaseService<AdminUserEntity>(db);
                var data= serives.GetAll().Include(e=>e.City).AsNoTracking().Where(e => e.PhoneNum == phoneNum);
                if (data.Count()<=0)
                {
                    return null;
                }
                else if(data.Count()==1)
                {
                   return DTO(data.SingleOrDefault());
                }
                else
                {
                    throw new ArgumentException("存在多个"+phoneNum+"的用户");
                }
            }
        }

        public void MarkDeleted(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<AdminUserEntity> serives = new BaseService<AdminUserEntity>(db);
                serives.MarkDeleted(id);
            }
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(db);
                var data = service.GetAll().Include(e => e.Roles).SingleOrDefault(e => e.Id == adminUserId);
                if (data==null)
                {
                    throw new ArgumentException("没有id为"+adminUserId+"的管理员用户");
                }
                return data.Roles.SelectMany(x => x.Permissions).Any(e => e.Name == permissionName);
            }
        }

        public void RecordLoginError(long id)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(db);
                var data= service.GetById(id);
                data.LoginErrorTimes = data.LoginErrorTimes++;
                db.SaveChanges();
            }
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void AddAdminUserRole(long id, long[] roleId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                var roles= service.GetAll().Include(e=>e.Permissions).Where(e => roleId.Contains(e.Id)).ToList();
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(db);
                var user = bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("不存在id为" + id + "的用户");
                }
                foreach (var item in roles)
                {
                    user.Roles.Add(item);
                }
                db.SaveChanges();
            }
        }


    }
}
