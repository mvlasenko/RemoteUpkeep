using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RemoteUpkeep.Startup))]
namespace RemoteUpkeep
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
