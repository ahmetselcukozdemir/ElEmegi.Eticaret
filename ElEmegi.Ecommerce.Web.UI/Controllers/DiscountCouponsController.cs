using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class DiscountCouponsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: DiscountCoupons
        public ActionResult Index()
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (Request.Cookies["admin_cerezim"] != null)
            {
                var data = db.DiscountCoupons.Where(x => x.IsActive == true).OrderByDescending(x => x.CreatedDate);
                return View(data.ToList());
            }
            else
            {
                return RedirectToAction("", "");
            }
        }

        // GET: DiscountCoupons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCoupon discountCoupon = db.DiscountCoupons.Find(id);
            if (discountCoupon == null)
            {
                return HttpNotFound();
            }
            return View(discountCoupon);
        }

        // GET: DiscountCoupons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscountCoupons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Percent,CreatedDate,ExpirationDate,IsActive")] DiscountCoupon discountCoupon)
        {
            if (ModelState.IsValid)
            {
                discountCoupon.CreatedDate = DateTime.Now;
                db.DiscountCoupons.Add(discountCoupon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discountCoupon);
        }

        // GET: DiscountCoupons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCoupon discountCoupon = db.DiscountCoupons.Find(id);
            if (discountCoupon == null)
            {
                return HttpNotFound();
            }
            return View(discountCoupon);
        }

        // POST: DiscountCoupons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Percent,CreatedDate,ExpirationDate,IsActive")] DiscountCoupon discountCoupon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discountCoupon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discountCoupon);
        }

        // GET: DiscountCoupons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCoupon discountCoupon = db.DiscountCoupons.Find(id);
            if (discountCoupon == null)
            {
                return HttpNotFound();
            }
            return View(discountCoupon);
        }

        // POST: DiscountCoupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiscountCoupon discountCoupon = db.DiscountCoupons.Find(id);
            db.DiscountCoupons.Remove(discountCoupon);
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
