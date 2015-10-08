using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Shared.Models.Interfaces;

namespace BusinessLogic
{
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
        }
    }
}
