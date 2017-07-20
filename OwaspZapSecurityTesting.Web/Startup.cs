using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OwaspZapSecurityTesting.Web.Startup))]
namespace OwaspZapSecurityTesting.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
