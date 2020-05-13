﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Core.Model.Entity;
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
        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.Where(i => i.ID == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().AddProduct(product, 1);
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

        public ActionResult UpdateCart(int Id)
        {
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());

        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();
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
                return View("Completed");
            }
            else
            {
                return View(entity);
            }

        }

        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
            var order = new Order();
            order.OrderNumber = "Spr" + (new Random()).Next(111111, 999999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            order.UserName = User.Identity.Name;
            order.AddressTitle = entity.AdresBasligi;
            order.Address = entity.Adres;
            order.City = entity.Sehir;
            order.District = entity.Semt;
            order.Neighborhood = entity.Mahalle;
            order.PostCode = entity.PostaKodu;

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