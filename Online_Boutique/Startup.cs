using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_Boutique.Startup))]
namespace Online_Boutique
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
