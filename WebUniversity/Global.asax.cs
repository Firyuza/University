using Castle.MicroKernel.Registration;

namespace WebUniversity
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using BusinessLogic;
    using Castle.Windsor;
    using Storage;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // создаем контейнер
            var container = new WindsorContainer();
            
            // регистрируем компоненты с помощью объекта Installer
            container.Install(new Installer());
            container.Register(Component.For<IWindsorContainer>().Instance(container));

            container.Install(new ControllerInstaller());
            
            // Вызываем свою фабрику контроллеров
            var castleControllerFactory = new WindsorControllerFactory(container);

            // Добавляем фабрику контроллеров для обработки запросов
            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);
        }
    }
}
