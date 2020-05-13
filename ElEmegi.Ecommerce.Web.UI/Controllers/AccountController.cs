using ElEmegi.Ecommerce.Core.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ElEmegi.Ecommerce.Web.UI.Models;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Account
        public ActionResult Index()
        {
       
           return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                user.IsActive = true;
                user.LastActivityDate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            var user = db.Users.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            if (user !=null)
            {
                HttpCookie cerez = new HttpCookie("cerezim");
                cerez.Values.Add("eposta", user.Email);
                cerez.Values.Add("ad", user.Name);
                cerez.Values.Add("soyad",user.Surname);
                cerez.Values.Add("ID",user.ID.ToString());
                cerez.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cerez);
              
            }

            Session["ad"] = user.Name;
            Session["soyad"] = user.Surname;
            Session["ID"] = user.ID.ToString();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Logout()
        {
            Session["eposta"] = null;
            Session["ID"] = null;
            Session["ad"] = null;
            Session["soyad"] = null;
            Session.Abandon();
            if (Request.Cookies["cerezim"] != null)
            {
                Response.Cookies["cerezim"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}