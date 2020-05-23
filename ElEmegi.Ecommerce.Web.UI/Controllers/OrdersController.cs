using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
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
    }
}