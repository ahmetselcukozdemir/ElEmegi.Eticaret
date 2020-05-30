using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Models;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class OrdersController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Orders
        public ActionResult Index()
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (admin_cerez != null)
            {
                int id = Convert.ToInt32(admin_cerez["id"]);
                var orders = db.Orders.Include("OrderLines").Where(x =>x.MemberID == id).ToList();
                return View(orders);
            }
            else
            {
                Response.Redirect("/Admin/Login/");
            }

            return View();
        }

        public ActionResult NotProcessedOrders()
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (admin_cerez != null)
            {
                int id = Convert.ToInt32(admin_cerez["id"]);
                var orders = db.Orders.Include("OrderLines").Where(x => x.MemberID == id && x.OrderState == EnumOrderState.Prepare).ToList();
                return View(orders);
            }
            else
            {
                Response.Redirect("/Admin/Login/");
            }
            return View();
        }

        public ActionResult OrdersInCargo()
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (admin_cerez != null)
            {
                int id = Convert.ToInt32(admin_cerez["id"]);
                var orders = db.Orders.Include("OrderLines").Where(x => x.MemberID == id && x.OrderState == EnumOrderState.Cargo).ToList();
                return View(orders);
            }
            else
            {
                Response.Redirect("/Admin/Login/");
            }
            return View();
        }

        public ActionResult Details(int id)
        {

            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (admin_cerez != null)
            {
                int member_id = Convert.ToInt32(admin_cerez["id"]);
                var data = db.Orders.Where(x => x.ID == id && x.MemberID == id)
                    .Select(x => new OrderDetailsModel()
                    {
                        OrderId = x.ID,
                        Name= x.Name,
                        Surname = x.Surname,
                        Email = x.Email,
                        OrderNumber = x.OrderNumber,
                        Total = x.Total,
                        OrderDate = x.OrderDate,
                        OrderState = x.OrderState,
                        AdresBasligi = x.AddressTitle,
                        Adres = x.Address,
                        Sehir = x.City,
                        Semt = x.District,
                        Mahalle = x.Neighborhood,
                        PostaKodu = x.PostCode,
                        OrderLines = x.OrderLines.Select(a=>new OrderLineModel()
                        {
                            ProductId = a.ProductID,
                            ProductName = a.Product.Name,
                            Image = a.Product.Image,
                            Quantity = a.Quantity,
                            Price = a.Price
                            
                        }).ToList()
                        
                    }).FirstOrDefault();
                return View(data);
            }
            else
            {
                Response.Redirect("/Admin/Login/");
            }

            return View();
        }

        public ActionResult UpdateOrderState(int orderId, EnumOrderState orderState)
        {
            var order = db.Orders.Where(i => i.ID == orderId).FirstOrDefault();
            if (order != null)
            {
                order.OrderState = orderState;
                db.SaveChanges();
                TempData["Message"] = "Bilgileriniz kaydedildi.";
                return RedirectToAction("Index", new { id = orderId });
            }
            return RedirectToAction("Index", "Orders");
        }
    }
}