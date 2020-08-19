using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WarmHome.Common
{
    public class AjaxResult<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public IEnumerable<T> data { get; set; }
        public string Redirect { get; set; }
    }
}
