using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WarmHome.Common
{
    public class AdminHelper
    {
        public static long? AdminUserId(HttpContextBase ctx)
        {
            return (long?)ctx.Session["AdminUserId"];
        }
    }
}
