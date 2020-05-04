using ElEmegi.Eticaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElEmegi.Eticaret.UI.WEB.Controllers
{
    public class ProductController : AndControllerBase
    {
        AndDB db = new AndDB();
        // GET: Product
        [Route("Urun/{title}/{id}")]
        public ActionResult Detail(string title,int id)
        {
            var product = db.Products.Where(x => x.ID == id).FirstOrDefault();
            return View(product);
        }
    }
}