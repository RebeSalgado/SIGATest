using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GardiSoft.Startup))]
namespace GardiSoft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
