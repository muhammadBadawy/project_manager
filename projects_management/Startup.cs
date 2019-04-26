using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(projects_management.Startup))]
namespace projects_management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
