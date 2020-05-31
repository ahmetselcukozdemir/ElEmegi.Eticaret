using System;
using System.Collections.Generic;
using System.Linq;
using ElEmegi.Ecommerce.API.Models;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(ElEmegi.Ecommerce.API.Startup))]
namespace ElEmegi.Ecommerce.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new LoginProvider()
            });
        }
    }
}
