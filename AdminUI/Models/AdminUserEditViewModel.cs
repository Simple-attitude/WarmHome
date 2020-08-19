using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using WarmHome.DTO;

namespace AdminUI.Models
{
    public class AdminUserEditViewModel
    {
        public AdminUserDTO User { get; set; }
        public IEnumerable<CityDTO> GetCityAll { get; set; }
        public IEnumerable<RoleDTO> GetRolesAll { get; set; }
        public IEnumerable<RoleDTO> AdminRoles { get; set; }
    }
}