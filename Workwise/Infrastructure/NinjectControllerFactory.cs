using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Workwise.Data.Interface;
using Workwise.Data;

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
            ninjectKernel.Bind<IPostRepository>().To<PostRepository>();
            ninjectKernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
            ninjectKernel.Bind<ICompanyRepository>().To<CompanyRepository>();

        }
    }
}