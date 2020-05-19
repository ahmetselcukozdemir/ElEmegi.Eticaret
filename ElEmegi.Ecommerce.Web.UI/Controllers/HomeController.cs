using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Core.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Models;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index() 
        {
            if (Request.Cookies["cerezim"] != null)
            {
                HttpCookie kayitlicerez = Request.Cookies["cerezim"];
                Session["eposta"] = kayitlicerez.Values["eposta"];
                Session["ad"] = kayitlicerez.Values["ad"];
                Session["soyad"] = kayitlicerez.Values["soyad"];
                Session["ID"] = kayitlicerez.Values["ID"].ToString();

            }
            var data = db.Products.OrderByDescending(x => x.IsApproved).Take(5).ToList();
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var product = db.Products.Where(x => x.ID == id).FirstOrDefault();
            var category = db.Categories.Where(x => x.ID == product.CategoryId).FirstOrDefault();
            ViewBag.ctgry = category;
            return View(product);
        }
        public PartialViewResult GetBestSellers()
        {
            var data = db.Products.OrderByDescending(x => x.IsApproved).Take(5).ToList();
            return PartialView(data);
        }

        public ActionResult Contact()
        {
            return View();
        }
     
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }


}
