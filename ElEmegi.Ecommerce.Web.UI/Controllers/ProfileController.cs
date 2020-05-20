using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser;
using ElEmegi.Ecommerce.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Models;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class ProfileController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Profile
        public ActionResult Index()
        {
            if (TempData["Status"] != null)
            {
                var data = TempData["Status"].ToString();
                ViewBag.status = data;
            }
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
        public ActionResult Edit(int id)
        {

            int user_id = Convert.ToInt32(Session["ID"]);
            var user = db.Users.Where(x => x.ID == user_id).FirstOrDefault();
            return View(user);

        }

        [HttpPost]
        public ActionResult Edit(User entity)
        {
            try
            {

                int id = Convert.ToInt32(Session["ID"]);
                var user = db.Users.Where(x => x.ID == id).FirstOrDefault();

                user.Name = entity.Name;
                user.Surname = entity.Surname;
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.LastActivityDate = DateTime.Now;
                db.SaveChanges();
                TempData["Status"] = "Success";

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users.Where(x => x.ID == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}