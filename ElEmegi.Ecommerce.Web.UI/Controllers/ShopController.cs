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
            var data = db.Products.Where(x => x.IsApproved == true && x.IsHome == true).OrderBy(x=>x.Name).ToPagedList(page ??1,10);
            var category = db.Categories.ToList();
            ViewBag.categoryList = category;
            return View(data);
        }

        public ActionResult FilterProductPrice(int? min, int? max)
        {
            var category = db.Categories.ToList();
            ViewBag.categoryList = category;
            if (min!=null && max !=null)
            {
                var data = db.Products.Where(x => x.Price >= min && x.Price <= max).ToList();
                return View(data);
            }
            return View();
        }
       
    }
}