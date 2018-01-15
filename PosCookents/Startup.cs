using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PosCookents.Startup))]
namespace PosCookents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
