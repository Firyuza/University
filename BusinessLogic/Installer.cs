namespace BusinessLogic
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Shared.Models.Interfaces;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IStudentService>()
            .ImplementedBy<StudentService>()
            .LifestyleTransient());

            container.Register(Component.For<IGroupService>()
            .ImplementedBy<GroupService>()
            .LifestyleTransient());

            container.Register(Component.For<IDepartmentService>()
            .ImplementedBy<DepartmentService>()
            .LifestyleTransient());

            container.Register(Component.For<IPositionService>()
            .ImplementedBy<PositionService>()
            .LifestyleTransient());

            container.Register(Component.For<ITeacherService>()
            .ImplementedBy<TeacherService>()
            .LifestyleTransient());

            container.Register(Component.For<ICourseService>()
            .ImplementedBy<CourseService>()
            .LifestyleTransient());

            container.Register(Component.For<IScheduleService>()
            .ImplementedBy<ScheduleService>()
            .LifestyleTransient());

            container.Register(Component.For<IAcademicProgressService>()
            .ImplementedBy<AcademicProgressService>()
            .LifestyleTransient());
        }
    }
}
