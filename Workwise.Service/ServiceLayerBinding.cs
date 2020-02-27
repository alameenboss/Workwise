using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using Workwise.Data;
using Workwise.Data.Interface;

namespace Workwise.Service
{
    public class ServiceLayerBinding : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IMessageRepository>().To<MessageRepository>();
            Kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
            Kernel.Bind<IPostRepository>().To<PostRepository>();
            Kernel.Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
