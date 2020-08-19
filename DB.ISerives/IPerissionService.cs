using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.ISerives
{
    public interface IPerissionService
    {
        void Edit(long id,string name,string desc);
        void Deleted(long id);
        void AddPermission(string name, string desc);
        PermissionDTO GetById(long id);
        IEnumerable<PermissionDTO> GetAll();
        IEnumerable<PermissionDTO> GetRoleId(long roleId);
        void AddPermissions(long roleId, long[] permisId);
        void UpdatePermissions(long roleId, long[] permisId);
    }
}
