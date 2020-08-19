using DB.ISerives;
using DB.Service;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.Serives
{
    class PermissionService : IPerissionService
    {
        public void AddPermissions(long roleId, long[] permisId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                var role= service.GetById(roleId);
                if (role==null)
                {
                    throw new ArgumentException("没用"+roleId+"的角色");
                }
                else
                {
                    BaseService<PermissionEntity> per = new BaseService<PermissionEntity>(db);
                    var permissions= per.GetAll().Where(e => permisId.Contains(e.Id)).ToList();
                    foreach (var item in permissions)
                    {
                        role.Permissions.Add(item);
                    }
                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<PermissionDTO> GetAll()
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<PermissionEntity> service = new BaseService<PermissionEntity>(db);
               return  service.GetAll().ToList().Select(item => GetDTO(item));
            }
        }
        public PermissionDTO GetDTO(PermissionEntity entity)
        {
            PermissionDTO permission = new PermissionDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                CreateDateTime = entity.CreateDateTime,
                Description = entity.Description
            };
            return permission;
        }
        public PermissionDTO GetById(long id)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<PermissionEntity> service = new BaseService<PermissionEntity>(db);
                return GetDTO(service.GetById(id));
            }
        }

        public IEnumerable<PermissionDTO> GetRoleId(long roleId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                return service.GetById(roleId).Permissions.Select(item => GetDTO(item));
            }
        }

        public void UpdatePermissions(long roleId, long[] permisId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                var role= service.GetById(roleId);
                if (role==null)
                {
                    throw new ArgumentException("没用"+roleId+"的角色数据");
                }
                else
                {
                    role.Permissions.Clear();
                    BaseService<PermissionEntity> per = new BaseService<PermissionEntity>(db);
                    var permissions= per.GetAll().Where(e => permisId.Contains(e.Id)).ToList();
                    foreach (var item in permissions)
                    {
                        role.Permissions.Add(item);
                    }
                    db.SaveChanges();
                }

            }
        }

        public void AddPermission(string name, string desc)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<PermissionEntity> service = new BaseService<PermissionEntity>(db);
                bool exists= service.GetAll().Any(e => e.Name == name);
                if (exists)
                {
                    throw new ArgumentException("此"+name+ "权限已存在");
                }
                PermissionEntity entity = new PermissionEntity()
                {
                    Name = name,
                    Description = desc
                };
                db.Permissions.Add(entity);
                db.SaveChanges();
            };
        }

        public void Deleted(long id)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<PermissionEntity> service = new BaseService<PermissionEntity>(db);
                service.MarkDeleted(id);
            }
        }

        public void Edit(long id, string name, string desc)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<PermissionEntity> service = new BaseService<PermissionEntity>(db);
                var per = service.GetAll().Where(e=>e.Id==id).SingleOrDefault(e => e.Name == name);
                if (per!=null)
                {
                    per.Name = name;
                    per.Description = desc;
                    db.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("不存在id为："+id+"的权限");
                }
            }
        }
    }
}
