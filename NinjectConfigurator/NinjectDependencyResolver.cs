using Dal;
using DalContracts;
using Logic;
using LogicCore;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NinjectConfigurator
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUserLogic>().To<UserLogic>().InSingletonScope();
            kernel.Bind<IUserDao>().To<UserDao>().InSingletonScope();
            kernel.Bind<IForumDao>().To<ForumDao>().InSingletonScope();
            kernel.Bind<IForumLogic>().To<ForumLogic>().InSingletonScope();
        }
    }
}
