using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser;
using ElEmegi.Ecommerce.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Models;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class ProfileController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Profile
        public ActionResult Index()
        {
            if (TempData["Status"] != null)
            {
                var data = TempData["Status"].ToString();
                ViewBag.status = data;
            }
            if (Request.Cookies["cerezim"] != null)
            {
                int id = Convert.ToInt32(Session["ID"]);
                var user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                return View(user);
            }
            else
            {
                RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {

            int user_id = Convert.ToInt32(Session["ID"]);
            var user = db.Users.Where(x => x.ID == user_id).FirstOrDefault();
            return View(user);

        }

        [HttpPost]
        public ActionResult Edit(User entity)
        {
            try
            {
                int id = Convert.ToInt32(Session["ID"]);
                var user = db.Users.Where(x => x.ID == id).FirstOrDefault();
                user.Name = entity.Name;
                user.Surname = entity.Surname;
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.LastActivityDate = DateTime.Now;
                db.SaveChanges();
                TempData["Status"] = "Success";

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        public ActionResult MyAddress()
        {
            int id = Convert.ToInt32(Session["ID"]);
            var address = db.UserAddress.Where(x => x.UserID == id);
            return View(address.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult MyAddress(UserAddress entity)
        {
            return View();
        }

        public ActionResult MyOrders()
        {
            if (Session["ID"] != null)
            {
                var user_id = Convert.ToInt32(Session["ID"]);
                var orders = db.Orders.Where(i => i.UserID == 1).Select(i => new UserOrderModel()
                {
                    ID = i.ID,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Total = i.Total
                }).OrderByDescending(i => i.OrderState).ToList();
                return View(orders.AsEnumerable());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult OrderDetails(int? id)
        {
            var entity = db.Orders.Where(i => i.ID == id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.ID,
                    OrderNumber = i.OrderNumber,
                    Total = i.Total,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Adres = i.Address,
                    AdresBasligi = i.AddressTitle,
                    Sehir = i.City,
                    Semt = i.District,
                    Mahalle = i.Neighborhood,
                    PostaKodu = i.PostCode,
                    OrderLines = i.OrderLines.Select(a => new OrderLineModel()
                    {
                        ProductId = a.ProductID,
                        ProductName = a.Product.Name.Length > 50 ? a.Product.Name.Substring(0, 47) + "..." : a.Product.Name,
                        Image = a.Product.Image,
                        Quantity = a.Quantity,
                        Price = a.Price
                    }).ToList()

                }).FirstOrDefault();
            return View(entity);
        }
    }
}