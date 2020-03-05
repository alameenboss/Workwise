using Microsoft.Owin;
using Ninject;
using Owin;
using Workwise.Infrastructure;
using Workwise.Helper;
using Workwise.ServiceAgent;
using Workwise.ServiceAgent.Interface;
using System;
using Microsoft.AspNet.SignalR;
using Workwise.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNet.SignalR.Hubs;

[assembly: OwinStartupAttribute(typeof(Workwise.Startup))]
namespace Workwise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }

    
}
