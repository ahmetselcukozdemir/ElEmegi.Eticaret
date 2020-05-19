using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Core.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class BlogController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Blog
        public ActionResult Index()
        {
            var data = db.Blogs.ToList();
            return View(data);
        }

        public ActionResult Details(int? id)
        {
            var data = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
    }
}