using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Workwise.ServiceAgent;
using Workwise.ServiceAgent.Interface;

namespace Workwise.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindgs();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindgs()
        {
            ninjectKernel.Bind<IPostServiceAgent>().To<PostServiceAgent>();
            ninjectKernel.Bind<IUserServiceAgent>().To<UserServiceAgent>();
            ninjectKernel.Bind<ICompanyServiceAgent>().To<CompanyServiceAgent>();
            ninjectKernel.Bind<IMessageServiceAgent>().To<MessageServiceAgent>();
            ninjectKernel.Bind<IHttpClient>().To<HttpClientWrapper>();
        }
    }
}