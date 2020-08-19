using DB.ISerives;
using DB.Service;
using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.Serives
{
    class RoleService : IRoleService
    {
        public IEnumerable<RoleDTO> GetAdminRoles(long id)
        {
            using (WarmHomeContext db = new WarmHomeContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(db);
                var user= service.GetAll().Include(e=>e.Roles).SingleOrDefault(e=>e.Id==id);
                if (user==null)
                {
                    return null;
                }
                var roles= user.Roles.Select(item => GetDTO(item));
                return roles;
            }
        }

        public long AddRole(string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                var role=service.GetAll().SingleOrDefault(e => e.Name == name);
                if (role!=null)
                {
                    throw new ArgumentException("此"+ name + "角色已存在");
                }
                else
                {
                    RolesEntity entity = new RolesEntity();
                    entity.Name = name;
                    db.Roles.Add(entity);
                    db.SaveChanges();
                    return entity.Id;
                }
            }
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                return service.GetAll().ToList().Select(item=>GetDTO(item));
            }
        }
        public RoleDTO GetDTO(RolesEntity entity)
        {
            RoleDTO role = new RoleDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                CreateDateTime = entity.CreateDateTime
            };
            return role;
        }
        public RoleDTO GetById(long roleid)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                var role= service.GetById(roleid);
                return role==null ? null : GetDTO(role);
            }
        }

        public RoleDTO GetByName(string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                var role= service.GetAll().SingleOrDefault(e => e.Name == name);
                return role == null ? null : GetDTO(role);
            }
        }

        public void MarkDelted(long roleId)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                service.MarkDeleted(roleId);
            }
        }

        public void UpdataRole(long roleId, string name)
        {
            using (WarmHomeContext db=new WarmHomeContext())
            {
                BaseService<RolesEntity> service = new BaseService<RolesEntity>(db);
                var role=service.GetById(roleId);
                if (role==null)
                {
                    throw new ArgumentException("不存在" + roleId + "的角色");
                }
                else
                {
                    role.Name = name;
                    db.SaveChanges();
                }
            }
        }

        public void AddPermissions(long roleId, long[] permis)
        {
            throw new NotImplementedException();
        }
    }
}
