using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Net;
using TravelTime.Models;

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
