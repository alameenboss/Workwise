using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Workwise.Startup))]
namespace Workwise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
