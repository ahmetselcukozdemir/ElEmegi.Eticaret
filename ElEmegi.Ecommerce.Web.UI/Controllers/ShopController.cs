using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Core.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class ShopController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Shop
        public ActionResult Index()
        {
            var data = db.Products.Where(x => x.IsApproved == true && x.IsHome == true).ToList();
            var category = db.Categories.ToList();
            ViewBag.categoryList = category;
            return View(data);
        }

       
    }
}