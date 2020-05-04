using ElEmegi.Eticaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElEmegi.Eticaret.UI.WEB.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        AndDB db = new AndDB();
        // GET: Admin/AdminLogin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Email,string Password)
        {
            var data = db.Members.Where(x => x.Email == Email && x.Password == Password).ToList();
            if (data.Count == 1)
            {
                Session["AdminLoginMember"] = data.FirstOrDefault();
                return Redirect("/admin/");
            }
            else
            {
                //Hatalı Giriş
                return View();
            }
          
        }
    }
}