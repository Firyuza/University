using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core;
using Castle.Core.Resource;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace Storage
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        //контейнер
        public IWindsorContainer Container { get; protected set; }

        public WindsorControllerFactory(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
 
            this.Container = container;
        }
 
        //получение контроллера для обработки запроса
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }
            // получаем запрошенный контроллер от Castle
            return Container.Resolve<IController>(controllerType.FullName);
        }
 
        // освобождаем контроллер
        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;
            if (disposableController != null)
            {
                disposableController.Dispose();
            }
 
            // информируем ioc-контейнер, что контроллер нам больше не нужен
            Container.Release(controller);
        }
    }    
}
