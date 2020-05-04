using ElEmegi.Eticaret.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElEmegi.Eticaret.UI.WEB.Controllers
{
    public class BasketController : AndControllerBase
    {
        AndDB db = new AndDB();
        // POST: Basket
        [HttpPost]
        public JsonResult AddProduct(int productID,int quantity)
        {
            db.Baskets.Add(new Core.Model.Entity.Basket
            {
                CreateDate = DateTime.Now,
                CreateUserID = LoginUserID,
                ProductID = productID,
                Quantity = quantity,
                UserID = LoginUserID
            });
           var rt = db.SaveChanges();
           return Json(rt,JsonRequestBehavior.AllowGet);
        }
        [Route("Sepetim")]
        public ActionResult Index()
        {
            var data = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var delete_item = db.Baskets.Where(x => x.ID == id).FirstOrDefault(); 
            db.Baskets.Remove(delete_item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
    
}