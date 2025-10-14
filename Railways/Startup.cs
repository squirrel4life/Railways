using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Railways.Startup))]
namespace Railways
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
