using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Core.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class AdminProductController : Controller
    {
        private DataContext db = new DataContext();

        // GET: AdminProduct
        public ActionResult Index()
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            int id = Convert.ToInt32(admin_cerez["id"]);
            if (admin_cerez != null)
            {
                var products = db.Products.Include(p => p.Category).Include(p => p.Member).Where(x => x.MemberID == id);
                return View(products.ToList());
            }
            else
            {
                Response.Redirect("/Admin/Login/");
            }

            return View();
        }

        // GET: AdminProduct/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Where(x => x.EncryptedString == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();  
            }
            return View(product);
        }

        // GET: AdminProduct/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: AdminProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EncryptedString,Name,Description,DescriptionTwo,Price,Image,Stock,IsApproved,IsHome,CategoryId,MemberID")] Product product)
        {
           
         
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (ModelState.IsValid)
            {
                if (admin_cerez != null)
                {
                    product.MemberID = Convert.ToInt32(admin_cerez["id"]);
                }
                else
                {
                    Response.Redirect("/Admin/Login/");
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
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", product.CategoryId);
            return View(product);
        }


        // GET: AdminProduct/Edit/5
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
                ViewBag.MemberID = new SelectList(db.Members, "ID", "Name", product.MemberID);
                return View(product);
           
           
        }

        // POST: AdminProduct/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,DescriptionTwo,Price,Image,Stock,IsApproved,IsHome,CategoryId,MemberID,EncryptedString")] Product product)
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (ModelState.IsValid)
            {
                if (admin_cerez != null)
                {
                    var id = Convert.ToInt32(admin_cerez["id"]);
                    product.MemberID = id;
                }
                else
                {
                    Response.Redirect("/Admin/Login/");
                }


                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", product.CategoryId);

            return View(product);
        }

        // GET: AdminProduct/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: AdminProduct/Delete/5
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
