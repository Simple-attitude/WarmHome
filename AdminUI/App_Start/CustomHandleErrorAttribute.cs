using log4net;
using System;
using System.Web.Mvc;

namespace AdminUI.App_Start
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public ILog log = LogManager.GetLogger(nameof(CustomHandleErrorAttribute));

        /// <summary>在发生异常时调用。</summary>
        /// <param name="filterContext">操作筛选器上下文。</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="filterContext" /> 参数为 null。</exception>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            Exception exception = filterContext.Exception;
            string url = filterContext.HttpContext.Request.RawUrl;
            log.ErrorFormat("页面出现异常：url:{0},异常信息:{1}", url, exception.Message);
            filterContext.Result = new RedirectResult("~/Error/HttpError500");
        }
    }
}