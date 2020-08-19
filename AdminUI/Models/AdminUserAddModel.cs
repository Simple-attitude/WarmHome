using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminUI.Models
{
    public class AdminUserAddModel
    {
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
        public long? City { get; set; }
        public string Email { get; set; }
        public long[] Roles { get; set; }

    }
}