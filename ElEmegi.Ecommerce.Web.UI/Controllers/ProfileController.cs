using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser;
using ElEmegi.Ecommerce.Core.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Models;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class ProfileController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Profile
        public ActionResult Index()
        {
            if (Request.Cookies["cerezim"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                var user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                return View(user);
            }
            else
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ProfileEdit()
        {
            if (Session["ID"] !=null )
            {
                int id = Convert.ToInt32(Session["ID"]);
                var user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();

        }

        [HttpPost]
        public ActionResult ProfileEdit(User entity, HttpPostedFileBase file)
        {
            int id = Convert.ToInt32(Session["ID"]);
            var user = db.Users.Find(entity.ID);
            if (file != null && file.ContentLength > 01)
            {
                var path = Path.Combine(Server.MapPath("~/Upload_Edilecek_Klasörümüz"), file.FileName);
                file.SaveAs(path);
                user.Name = entity.Name;
                user.Surname = entity.Surname;
                user.Email = entity.Email;
                user.Password = user.Password;
                user.Photo = path;
                db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                user.Name = entity.Name;
                user.Surname = entity.Surname;
                user.Email = entity.Email;
                user.Password = user.Password;
                db.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }

        public ActionResult MyOrders()
        {
            if (Session["ID"] != null)
            {
                int user_id = Convert.ToInt32(Session["ID"]);
                var orders = db.Orders.Where(i => i.UserID == user_id && i.OrderUserControl == true).OrderByDescending(i => i.OrderState).ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}