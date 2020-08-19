using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminUI.Models
{
    public class AdminUserEditModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public long[] RoleIds { get; set; }
        public string Password { get; set; }
        public long? CityId { get; set; }

    }
}