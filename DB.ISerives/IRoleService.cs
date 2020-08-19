using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarmHome.DTO;

namespace DB.ISerives
{
    public  interface IRoleService
    {
        RoleDTO GetById(long roleid);
        long AddRole(string name);
        RoleDTO GetByName(string name);
        void UpdataRole(long roleId, string name);
        IEnumerable<RoleDTO> GetAll();
        void MarkDelted(long roleId);
        void AddPermissions(long roleId, long[] permis);
        IEnumerable<RoleDTO> GetAdminRoles(long id);
    }
}
