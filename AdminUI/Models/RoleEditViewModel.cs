using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmHome.DTO;

namespace AdminUI.Models
{
    public class RoleEditViewModel
    {
        public RoleDTO Role { get; set; }
        public IEnumerable<PermissionDTO> AllPermissions { get; set; }
        public IEnumerable<PermissionDTO> RolePermissions { get; set; }
    }
}