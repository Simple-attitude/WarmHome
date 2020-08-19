using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminUI.App_Start
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CheckPermissionsAttribute:Attribute
    {
        public string Permission { get; set; }
        public CheckPermissionsAttribute(string permission)
        {
            this.Permission = permission;
        }
    }
}