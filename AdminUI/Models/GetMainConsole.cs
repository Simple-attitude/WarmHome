using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace AdminUI.Models
{
    public class GetMainConsole
    {
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
        public long FullHouseCount { get; set; }
        public long JoinHouseCount { get; set; }
    }
}