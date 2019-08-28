using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MainWeb.Startup))]
namespace MainWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
