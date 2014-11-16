using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cajovna.Startup))]
namespace Cajovna
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
