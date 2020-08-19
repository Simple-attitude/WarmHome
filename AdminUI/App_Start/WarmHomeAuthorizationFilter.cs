using DB.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WarmHome.Common;

namespace AdminUI.App_Start
{

    public class WarmHomeAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)          
        {
            IAdminUserService adminService = DependencyResolver.Current.GetService<IAdminUserService>();

            //获取Action上的Attribte
            CheckPermissionsAttribute[] permis=(CheckPermissionsAttribute[])filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckPermissionsAttribute), false);
            if (permis.Length<=0)
            {
                return;
            }
            long? adminUserId = (long ?)filterContext.HttpContext.Session["AdminUserId"];
            if (adminUserId==null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AjaxResult<string> ajax = new AjaxResult<string>
                    {
                        code = 2,
                        msg = "未登录",
                        data = null,
                        Redirect = "~/Main/login"
                    };
                    filterContext.Result = new JsonNetResult() { Data = ajax };
                }
                filterContext.Result = new RedirectResult("~/Main/login");
                return;
            }
            foreach (var perm in permis)
            {
                if (!adminService.HasPermission((long)adminUserId, perm.Permission))
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        AjaxResult<string> ajax = new AjaxResult<string>
                        {
                            code = 3,
                            msg = "没有此权限，请联系管理员！"
                        };
                        filterContext.Result = new JsonNetResult() { Data = ajax };
                    }
                    filterContext.Result = new ContentResult() { Content = "没有权限" };
                } 
            }
        }
    }
}