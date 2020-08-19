using AdminUI.App_Start;
using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WarmHome.Common;

namespace AdminUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前            程序集中的 Controller都注册
            //不要忘了.PropertiesAutowired()
            //获取所有相关类库的程序集
            Assembly[] assemblies = new Assembly[] { Assembly.Load("DB.Serives") };
            builder.RegisterAssemblyTypes(assemblies)
            .Where(type => !type.IsAbstract)
            .AsImplementedInterfaces().PropertiesAutowired();
            var container = builder.Build();
            //注册系统级别的  DependencyResolver，这样当 MVC框架创建   Controller等对象的时候都           是管 Autofac要对象。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalFilters.Filters.Add(new JsonNetActionFilter());
            GlobalFilters.Filters.Add(new WarmHomeException());
            GlobalFilters.Filters.Add(new WarmHomeAuthorizationFilter());
            GlobalFilters.Filters.Add(new CustomHandleErrorAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
