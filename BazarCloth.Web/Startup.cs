using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BazarCloth.Web.Startup))]
namespace BazarCloth.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
