using ElEmegi.Eticaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ElEmegi.Eticaret.UI.WEB
{
    public class AndControllerBase : Controller
    {
        //Kullanıcı Login Kontrolü
        public bool IsLogin{ get; private set; }
        //Giriş yapan kişinin ID'si
        public int LoginUserID { get; private set; }
        public User LoginUserEntity{ get; private set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Session["LoginUserID"] != null)
            {
                //kullanıcı giriş yapmış ise ;
                IsLogin = true;
                LoginUserID = (int)requestContext.HttpContext.Session["LoginUserID"];
                LoginUserEntity = (Core.Model.Entity.User)requestContext.HttpContext.Session["LoginUser"];
            }
            base.Initialize(requestContext);
        }
    }
}