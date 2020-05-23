using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class OrdersController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Orders
        public ActionResult Index()
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (admin_cerez != null)
            {
                int id = Convert.ToInt32(admin_cerez["id"]);
                var orders = db.Orders.Include("Products").Where(x =>x.UserID == id);
                return View(orders.ToList());
            }
            else
            {
                Response.Redirect("/Admin/Login/");
            }

            return View();
        }

        public ActionResult NotProcessedOrders()
        {
            return View();
        }

        public ActionResult OrdersInCargo()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}