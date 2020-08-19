using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service.Entitys
{
    public class PermissionEntity:BaseEntity
    {
        public string Name { get; set; }//名称
        public string Description { get; set; }//权限项说明

        public ICollection<RolesEntity> Roles { get; set; } = new List<RolesEntity>();//一个权限对应多个角色
    }
}
