using DB.ISerives;
using DB.Service;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.Common;
using WarmHome.DTO;

namespace DB.Serives
{
    class UserService : IUserService
    {
        public long AddNew(string phoneNum, string password)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<UserEntity> service = new BaseService<UserEntity>(db);
                bool exists= service.GetAll().Any(s => s.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("手机号已存在");

                }
                else
                {
                    string salt = CommonHelper.CreateVerifyCode(4);
                    string pwd = CommonHelper.CalcMD5(password + salt);
                    UserEntity entity = new UserEntity()
                    {
                        PasswordSalt = salt,
                        PasswordHash = pwd,
                        PhoneNum = phoneNum,
                    };
                    db.Users.Add(entity);
                    return entity.Id;
                }
            }
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<UserEntity> service = new BaseService<UserEntity>(db);
                var user= service.GetAll().SingleOrDefault(e => e.PhoneNum == phoneNum);
                if (user==null)
                {
                    return false;
                }
                string pwd = CommonHelper.CalcMD5(password + user.PasswordSalt);
                return user.PasswordHash == pwd ? true : false;
            }
        }

        public UserDTO GetById(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<UserEntity> service = new BaseService<UserEntity>(db);
                var data = service.GetById(id);
                if (data==null)
                {
                    return null;
                }
                return GetDTO(data);
            }
        }
        public UserDTO GetDTO(UserEntity entity)
       {
            UserDTO user = new UserDTO()
            {
                Id = entity.Id,
                CityId = entity.CityId,
                CreateDateTime = entity.CreateDateTime,
                LoginErrorTime = entity.LoginErrorTime,
                PhoneNum = entity.PhoneNum,
                
                LastLoginErrorDateTime = entity.LastLoginErrorTime
            };
            return user;
        }

        public UserDTO GetByPhoneNum(string phoneNum)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<UserEntity> service = new BaseService<UserEntity>(db);
                var user= service.GetAll().SingleOrDefault(e=>e.PhoneNum==phoneNum);
                if (user==null)
                {
                    return null;
                }
                return GetDTO(user);
            }
        }

        public void SetUserCityId(long userId, long cityId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<UserEntity> service = new BaseService<UserEntity>(db);
                var user= service.GetById(userId);
                if (user==null)
                {
                    throw new ArgumentException("不存在Id为" + userId + "的用户");
                }
                user.CityId = cityId;
                db.SaveChanges();
            }
        }

        public void UpdatePwd(long userId, string newPassword)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<UserEntity> service = new BaseService<UserEntity>(db);
                var user= service.GetById(userId);
                if (user==null)
                {
                    throw new ArgumentException("不存在Id为" + userId + "的用户");
                }
                string pwd = CommonHelper.CalcMD5(newPassword + user.PasswordSalt);
                user.PasswordHash = pwd;
                db.SaveChanges();
            }
        }
    }
}
