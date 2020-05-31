using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ElEmegi.Ecommerce.Web.UI.App_Start.Startup1))]

namespace ElEmegi.Ecommerce.Web.UI.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
