using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class ProductsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Products
        public ActionResult Index()
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (admin_cerez != null)
            {
                int id = Convert.ToInt32(admin_cerez["id"]);
                var products = db.Products.Include(p => p.Category).Include(p => p.Member).Where(x => x.MemberID == id);
                return View(products.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Include(p=>p.Category).Where(x => x.EncryptedString == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Product product, IEnumerable<HttpPostedFileBase> images)
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (ModelState.IsValid)
            {
                if (admin_cerez != null)
                {
                    product.MemberID = Convert.ToInt32(admin_cerez["id"]);
                }
                //EncryptedString
                char[] cr = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
                string result = string.Empty;
                Random r = new Random();
                for (int i = 0; i < 15; i++)
                {
                    result += cr[r.Next(0, cr.Length - 1)].ToString();
                }
                product.EncryptedString = result;
                product.CreatedDate = DateTime.Now;

                //images list 
                
                var image_one = System.IO.Path.GetFileName(images.ElementAt(0).FileName);
                var image_two = System.IO.Path.GetFileName(images.ElementAt(1).FileName);
                var image_three = System.IO.Path.GetFileName(images.ElementAt(2).FileName);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Products.Find(id);
            var product = db.Products.Where(x => x.EncryptedString == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                var admin_cerez = Request.Cookies["admin_cerezim"];
                int id = Convert.ToInt32(admin_cerez["id"]);
                var data = db.Products.Where(x => x.EncryptedString == product.EncryptedString).FirstOrDefault();
                data.Image = product.Image;
                data.CategoryId = product.CategoryId;
                data.Description = product.Description;
                data.IsHome = product.IsHome;
                data.Name = product.Name;
                data.Price = product.Price;
                data.IsHome = product.IsHome;
                data.ImageTwo = product.ImageTwo;
                data.ImageThree = product.ImageThree;
                data.IsApproved = data.IsApproved;
                ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", product.CategoryId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ViewBag.ErrorMessage = "Güncelleme işleminde bir hata oluştu. :( Hata kodu =  "+e+"";
                throw;
            }
            
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
