using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminUI.App_Start
{
    public class WarmHomeException : IExceptionFilter
    {
        private static ILog Log = LogManager.GetLogger(typeof(WarmHomeException));
        public void OnException(ExceptionContext filterContext)
        {
            Log.ErrorFormat("出现未处理异常:{0}", filterContext.Exception);
        }
    }
}