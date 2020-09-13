using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SJBugTracker.Startup))]
namespace SJBugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
