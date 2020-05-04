using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElEmegi.Eticaret.UI.WEB.Areas.Admin
{
    public class AdminControllerBase : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var IsLogin = false;
            if (requestContext.HttpContext.Session["AdminLoginMember"] == null)
            {
                //admin girişi yoksa
                requestContext.HttpContext.Response.Redirect("/Admin/AdminLogin/");
            }
            else
            {
                base.Initialize(requestContext);
            }
        }
    }
}