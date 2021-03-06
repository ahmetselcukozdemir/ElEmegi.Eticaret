﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ElEmegi.Ecommerce.Model.Entity;
using ElEmegi.Ecommerce.Web.UI.Models;
using Newtonsoft.Json;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();
        [HttpGet]
        // GET: Admin
        public ActionResult Index()
        {
            var total_product = db.Products.Count();
            ViewBag.total_product = total_product;
            var total_orders = db.Orders.Count();
            ViewBag.total_orders = total_orders;

            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (Request.Cookies["admin_cerezim"] != null)
            {
                int id = Convert.ToInt32(admin_cerez["id"]);
                var member = db.Members.Where(x => x.ID == id).FirstOrDefault();

                var orders = db.Orders.Select(i => new AdminOrderModel()
                {
                    Id = i.ID,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Total = i.Total,
                    Count = i.OrderLines.Count,
                    MemberID = i.MemberID,

                }).Where(x => x.MemberID == id).ToList();

                ViewBag.completed = orders.Where(x => x.OrderState == EnumOrderState.Completed).Count();
                ViewBag.orders_waiting = orders.Where(x => x.OrderState == EnumOrderState.Waiting).Count();
                ViewBag.orders_prepare = orders.Where(x => x.OrderState == EnumOrderState.Prepare).Count();

                if (member != null)
                {
                    var count_products = db.Products.Where(x => x.MemberID == id).ToList();
                    ViewBag.my_products_count = count_products.Count;
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success && ModelState.IsValid)
            {
                var member = db.Members.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                if (member != null)
                {
                    HttpCookie admincerez = new HttpCookie("admin_cerezim");
                    //admincerez.Values.Add("admin_ad", member.Name);
                    //admincerez.Values.Add("admin_surname", member.Surname);
                    //admincerez.Values.Add("admin_ID", member.ID.ToString());
                    admincerez["id"] = member.ID.ToString();
                    admincerez["name"] = member.Name;
                    admincerez["photo"] = member.Photo;
                    admincerez["isAdmin"] = member.IsAdmin.ToString();
                    admincerez.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(admincerez);
                }
                else
                {
                    ViewBag.ErrorMessage = "Kullanıcı adınız veya şireniz hatalı.";
                    return View();
                }

                //Session["admin_name"] = member.Name;
                //Session["admin_surname"] = member.Surname;
                //Session["admin_ID"] = member.ID.ToString();
                return RedirectToAction("Index", "Admin", "");
            }
            else
            {
                TempData["Status"] = "Lütfen robot olmadığınızı belirtmek için testi geçiniz.";
                return RedirectToAction("Login", "Admin");
            }
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["admin_cerezim"] != null)
            {
                Response.Cookies["admin_cerezim"].Expires = DateTime.Now.AddHours(-1);
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult ProductComments()
        {
            var data = db.ProductComments.Where(x => x.IsApproved == false);
            return View(data.ToList());
        }

        public ActionResult EditProductComments(int id)
        {
            var data = db.ProductComments.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult UpdateProductComment(ProductComments entityComments)
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            var data = db.ProductComments.Where(x => x.ID == entityComments.ID).FirstOrDefault();
            if (data != null)
            {
                data.IsApproved = entityComments.IsApproved;
                db.SaveChanges();
                return RedirectToAction("ProductComments", "Admin");
            }

            return View();
        }

        public ActionResult DeleteProductComment(int id)
        {
            var data = db.ProductComments.Where(x => x.ID == id).FirstOrDefault();
            if (data != null)
            {
                db.ProductComments.Remove(data);
                db.SaveChanges();
            }
            return RedirectToAction("ProductComments", "Admin");
        }
        public ActionResult MemberInformation()
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            if (Request.Cookies["admin_cerezim"] != null)
            {
                int id = Convert.ToInt32(admin_cerez["id"]);
                var member = db.Members.Where(x => x.ID == id).FirstOrDefault();
                if (member == null)
                {
                    return RedirectToAction("Login", "Admin");
                }
                return View(member);
            }
            return View();
        }

        public void UpdateMember(Member entity, HttpPostedFileBase image)
        {
            var admin_cerez = Request.Cookies["admin_cerezim"];
            int id = Convert.ToInt32(admin_cerez["id"]);
            var member = db.Members.Where(x => x.ID == id).FirstOrDefault();
            if (id != null && member != null)
            {
                member.Email = entity.Email;
                member.Name = entity.Name;
                member.Surname = entity.Surname;
                member.Photo = entity.Photo;
                member.Phone = entity.Phone;
                member.Password = entity.Password;
                if (image.ContentLength > 0 || image.FileName != null)
                {
                    double FileSize = Convert.ToDouble(image.ContentLength / 1024);
                    if (FileSize > 10240)
                    {
                        ViewBag.SizeError = "Fotoğraf 10 mb'dan büyük olamaz.";
                    }
                    string image_name = System.IO.Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/images/profiles/" + image_name));
                    image.SaveAs(path);
                    member.Photo = image_name;
                }
                else
                {
                    ViewBag.Error = "Resim yüklenirken bir hata oluştu. Lütfen bilgilerinizi kontrol ediniz.";
                }
                db.SaveChanges();
                ViewBag.SuccessMessage = "Bilgileriniz başarıyla güncellenmiştir.";
            }
            RedirectToAction("MemberInformation").ExecuteResult(this.ControllerContext);
        }
        public ActionResult ForgotMyPassword()
        {
            return View();
        }

        public ActionResult ResetPasswordMail(string email)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success)
            {


                var member = db.Members.Where(x => x.Email == email).FirstOrDefault();
                if (member != null)
                {
                    char[] cr = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
                    string result = string.Empty;
                    Random r = new Random();
                    for (int i = 0; i < 8; i++)
                    {
                        result += cr[r.Next(0, cr.Length - 1)].ToString();
                    }
                    HttpCookie cerez = new HttpCookie("reset_password_admin");
                    cerez.Values.Add("code", Server.UrlEncode(result));
                    cerez.Values.Add("reset_member_id", Server.UrlEncode(member.ID.ToString()));
                    cerez.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cerez);

                    Session["password_id"] = result;
                    Mail mail = new Mail();
                    mail.ForgotMyPassword(email, result);
                }
                return RedirectToAction("NewPassword", "Admin", new { member_id = member.ID });
            }
            else
            {
                ViewBag.error = "Robot testini geçmeniz gerekiyor.";
            }
            return View();
        }
        [HttpGet]
        public ActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(string password, string repassword, string reset_code)
        {
            var reset_cerez_admin = Request.Cookies["reset_password_admin"];
            var memberid = reset_cerez_admin["reset_member_id"];
            var data = db.Members.Where(x => x.ID.ToString() == memberid).FirstOrDefault();
            var code = reset_cerez_admin["code"];
            if (data != null && password == repassword)
            {
                if (code == reset_code)
                {
                    data.Password = password;
                    db.SaveChanges();
                    Response.Cookies["reset_password_admin"].Expires = DateTime.Now.AddDays(-1);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Şifreleriniz uyuşmuyor ya da böyle bir kullanıcı bulunamadı.";
            }
            ViewData["Success"] = "Şifreniz başarılı bir şekilde güncellenmiştir.";
            return RedirectToAction("Login", "Admin");
        }
        [HttpPost]
        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }
    }
}