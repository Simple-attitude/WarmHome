using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    /// <summary>
    /// 角色表
    /// </summary>
    public  class RolesEntity:BaseEntity
    {
        public string Name { get; set; }//名称
        public  ICollection<AdminUserEntity> AdminUsers { get; set; } = new List<AdminUserEntity>();//一个角色对应多个管理员
        public virtual ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();//一个角色又多个权限
    }
}
