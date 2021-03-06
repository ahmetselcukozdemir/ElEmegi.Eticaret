﻿using ElEmegi.Eticaret.Core.Model;
using ElEmegi.Eticaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElEmegi.Eticaret.UI.WEB.Controllers
{
    public class HomeController : AndControllerBase
    {
        AndDB db = new AndDB();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.IsLogin = this.IsLogin;
            var data = db.Products.OrderByDescending(x => x.CreateDate).Take(5).ToList();
            return View(data);
        }

        public PartialViewResult GetIsActiveCategories()
        {
            var categories = db.Categories.Where(x => x.ParentID == 0 && x.IsActive == true).ToList();
            return PartialView(categories);
        }

        public PartialViewResult GetCartProductList()
        {
            var baskets = db.Baskets.Include("Product").Where(x => x.ProductID == LoginUserID).ToList();
            return PartialView(baskets);
        }

        [Route("Uye-Giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Uye-Giris")]
        public ActionResult Login(string Email, string Password)
        {
            var users = db.Users.Where(x => x.Email == Email && x.Password == Password && x.IsActive == true).ToList();
            if (users.Count == 1)
            {
                Session["LoginUserID"] = users.FirstOrDefault().ID;
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("/");
            }
            else
            {
                ViewBag.Error = "Email veya şifre hatalı.";
                return View();
            }
        }

        [HttpGet]
        [Route("Uye-Kayit")]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Route("Uye-Kayit")]
        public ActionResult CreateUser(User entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateUserID = 1;
                entity.IsActive = true;
                db.Users.Add(entity);
                db.SaveChanges();
                return Redirect("/");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();

        }
    }
}