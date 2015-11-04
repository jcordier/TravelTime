using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelTime.Startup))]
namespace TravelTime
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
