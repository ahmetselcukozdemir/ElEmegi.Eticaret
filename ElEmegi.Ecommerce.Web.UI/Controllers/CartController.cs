﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Models;
namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public PartialViewResult CartProduct()
        {
            return PartialView(GetCart());
        }
        public ActionResult AddToCart(int Id, int text1)
        {
            var product = db.Products.Where(i => i.ID == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().AddProduct(product, text1);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.Where(i => i.ID == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateFromCart(int Id, int quantity)
        {
            var product = db.Products.Where(x => x.ID == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().UpdateCart(product, quantity);
            }
            return RedirectToAction("Index");
        }

        public ActionResult DiscountCouponFromCard(string coupon_code)
        {
            if (coupon_code != null)
            {
                var coupon = db.DiscountCoupons.Where(x => x.Name == coupon_code).FirstOrDefault();
                if (coupon != null)
                {
                    Session["percent"] = coupon.Percent;
                    Session["coupon"] = coupon_code;
                    GetCart().CouponDiscount(coupon.Percent);
                }
                else
                {
                    Session["percent"] = null;
                    Session["coupon"] = null;
                    ViewBag.CouponError = "İndirim kuponunuz hatalı veya kullanım süresi geçmiş olabilir";
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {
            //if (Request.Cookies["cerezim"] == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //else
            //{
            //    ViewBag.cart = GetCart();
            //    return View(new ShippingDetails());
            //}
            ViewBag.cart = GetCart();
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();
            if (cart != null)
            {
                ViewBag.cart = GetCart();
            }
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("NotProductError", "Sepetinizde ürün bulunmamaktadır.");
            }
            if (ModelState.IsValid)
            {
                //siparişi veritabanına kayıt et.
                SaveOrder(cart, entity);
                //cartı sıfırla.
                cart.Clear();
                ViewBag.ErrorMessage = "Lütfen sözleşmeyi onaylayın.";
                Session["coupon"] = null;
                Session["percent"] = null;
                return View("Completed");
            }
            return View(entity);
        }
        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
            if (Request.Cookies["cerezim"] != null)
            {
                var order = new Order();
                order.OrderNumber = "Spr" + (new Random()).Next(111111, 999999).ToString();
                order.Total = cart.Total();
                order.OrderDate = DateTime.Now;
                order.OrderState = EnumOrderState.Waiting;
                order.Name = entity.Name;
                order.Surname = entity.Surname;
                order.AddressTitle = entity.AdresBasligi;
                order.Address = entity.Adres;
                order.City = entity.Sehir;
                order.District = entity.Semt;
                order.Neighborhood = entity.Mahalle;
                order.PostCode = entity.PostaKodu;
                order.OrderUserControl = true;
                order.Email = entity.Email;
                order.UserID = Convert.ToInt32(Session["ID"]);
                order.OrderLines = new List<OrderLine>();
                foreach (var pr in cart.CartLines)
                {
                    OrderLine ordeline = new OrderLine();
                    ordeline.Quantity = pr.Quantity;
                    ordeline.Price = pr.Quantity * pr.Product.Price;
                    ordeline.ProductID = pr.Product.ID;
                    order.OrderLines.Add(ordeline);
                }
                db.Orders.Add(order);
                db.SaveChanges();
                Mail email = new Mail();
                //email.Mail(order.Email);
                //email.OrderMail(order.Email, order.OrderNumber, order.Total.ToString());

            }
            else
            {
                var order = new Order();
                order.OrderNumber = "Spr" + (new Random()).Next(111111, 999999).ToString();
                order.Total = cart.Total();
                order.OrderDate = DateTime.Now;
                order.OrderState = EnumOrderState.Waiting;
                order.Name = entity.Name;
                order.Surname = entity.Surname;
                order.AddressTitle = entity.AdresBasligi;
                order.Address = entity.Adres;
                order.City = entity.Sehir;
                order.District = entity.Semt;
                order.Neighborhood = entity.Mahalle;
                order.PostCode = entity.PostaKodu;
                order.OrderUserControl = false;
                order.Email = entity.Email;
                order.OrderLines = new List<OrderLine>();
                foreach (var pr in cart.CartLines)
                {
                    OrderLine ordeline = new OrderLine();
                    ordeline.Quantity = pr.Quantity;
                    ordeline.Price = pr.Quantity * pr.Product.Price;
                    ordeline.ProductID = pr.Product.ID;
                    order.OrderLines.Add(ordeline);
                }

                db.SaveChanges();
                Mail orderEmail = new Mail();
                //orderEmail.OrderMail(order.Email, order.OrderNumber, order.Total.ToString());
                //orderEmail.Mail(order.Email);
            }
        }

        public static async Task<string> EmailTemplate()
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Template/NewUserMail.cshtml");
            StreamReader objStreamReader = new StreamReader(templateFilePath);
            var body = await objStreamReader.ReadToEndAsync();
            objStreamReader.Close();
            return body;
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