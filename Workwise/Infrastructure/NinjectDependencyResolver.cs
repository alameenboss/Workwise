using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent;
using Workwise.ServiceAgent.Interface;
namespace Workwise.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(kernel);
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
            kernel.Bind<IPostServiceAgent>().To<PostServiceAgent>();
            kernel.Bind<IUserServiceAgent>().To<UserServiceAgent>();
            kernel.Bind<ICompanyServiceAgent>().To<CompanyServiceAgent>();
            kernel.Bind<IMessageServiceAgent>().To<MessageServiceAgent>();
            kernel.Bind<IHttpClient>().To<HttpClientWrapper>();
            kernel.Bind<IDefaultsHelper>().To<DefaultsHelper>();
        }
    }

   
}