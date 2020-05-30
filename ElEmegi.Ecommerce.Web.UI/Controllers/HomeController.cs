using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ElEmegi.Ecommerce.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Models;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var populer_categories = db.Categories.OrderByDescending(x => x.Products.Count).Take(3).ToList();
            ViewBag.cat_populer = populer_categories;
            if (Request.Cookies["cerezim"] != null)
            {
                HttpCookie kayitlicerez = Request.Cookies["cerezim"];
                Session["eposta"] = kayitlicerez.Values["eposta"];
                Session["ad"] = kayitlicerez.Values["ad"];
                Session["soyad"] = kayitlicerez.Values["soyad"];
                Session["ID"] = kayitlicerez.Values["ID"].ToString();
            }
            var data = db.Products.OrderByDescending(x => x.IsApproved).Take(5).ToList();
            var blog = db.Blogs.Where(x => x.IsActive == true).ToList();
            ViewBag.blog = blog;
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var product = db.Products.Where(x => x.ID == id).FirstOrDefault();
            var related_products = db.Products.Where(x => x.CategoryId == product.CategoryId);
            ViewBag.related_product = related_products.ToList();
            return View(product);
        }

        public PartialViewResult GetBestSellers()
        {
            var data = db.Products.OrderByDescending(x => x.IsApproved).Take(5).ToList();
            return PartialView(data);
        }

        public PartialViewResult OpportunitiesOfTheDayProduct()
        {
            //var data = (from x in db.Orders orderby Guid.NewGuid() ascending select x).ToList();
            //var data = db.Orders.OrderBy(x=>Guid.NewGuid()).Take(5).ToList();
            var data = db.Products.Where(x => x.IsHome == true).ToList();
            return PartialView(data);
        }

        public PartialViewResult NewProducts()
        {
            var data = db.Products.OrderByDescending(x => DateTime.Now - x.CreatedDate).Take(6).ToList();
            return PartialView(data);
        }

        public ActionResult BlogPost()
        {
            var data = db.Blogs.Where(x => x.IsActive == true).ToList();
            return View(data);
        }

        public PartialViewResult _PartialBlog()
        {
            //var data = db.Blogs.Where(x=>x.IsActive == true).ToList();
            //if (data !=null)
            //{
            //    ViewBag.blog = data;
            //    return PartialView(data);
            //}
            return PartialView();
        }

        public ActionResult BlogDetails(int? id)
        {
            var blog = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
            return View(blog);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult CookiePolicy()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult OrderTracking(string order_id)
        {
            var order = db.Orders.Where(x => x.OrderNumber == order_id.ToString()).Include("OrderLines").FirstOrDefault();
            if (order != null)
            {
                return View(order);
            }
            else
            {
                ViewBag.ErrorOrder = "Sipariş bulunamadı,lütfen sipariş numaranızı kontrol edin.";
                return RedirectToAction("Index", "Home");
            }

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
