using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Model.Entity;
using PagedList;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class ShopController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Shop
        public ActionResult Index(int? page)
        {
            var data = db.Products.Where(x => x.IsApproved == true && x.IsHome == true).OrderBy(x=>x.Name).ToPagedList(page ??1,3);
            var category = db.Categories.ToList();
            ViewBag.categoryList = category;
            return View(data);
        }

        public ActionResult FilterProduct()
        {
            return View();
        }
       
    }
}