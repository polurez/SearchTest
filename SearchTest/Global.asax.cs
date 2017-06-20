using Castle.Windsor;
using SearchTest.Castle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SearchTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new WindsorContainer();
            // регистрируем компоненты с помощью объекта ApplicationCastleInstaller
            container.Install(new ApplicationCastleInstaller());

            // Вызываем свою фабрику контроллеров
            var castleControllerFactory = new CastleControllerFactory(container);

            // Добавляем фабрику контроллеров для обработки запросов
            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
