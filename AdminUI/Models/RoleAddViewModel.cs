using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminUI.Models
{
    public class RoleAddViewModel
    {
        public  string  Name { get; set; }
        public long[] Permissions { get; set; }

    }
}