using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CProject.Startup))]
namespace CProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
