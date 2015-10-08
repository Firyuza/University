namespace WebUniversity
{
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Controllers;

    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IController>()
            .ImplementedBy<HomeController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<StudentsController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<TeachersController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<GroupsController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<CoursesController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<AcademicProgressesController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<DepartmentsController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<ExaminationDatasheetsController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<PositionsController>()
            .LifestyleTransient());

            container.Register(Component.For<IController>()
            .ImplementedBy<SchedulesController>()
            .LifestyleTransient());
            /*
            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
             */
        }
    }
}