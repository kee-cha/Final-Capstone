using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalCapstone.Startup))]
namespace FinalCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
