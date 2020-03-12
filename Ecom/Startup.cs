using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ecom.Startup))]
namespace Ecom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
