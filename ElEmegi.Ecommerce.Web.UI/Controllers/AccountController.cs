using ElEmegi.Ecommerce.Model.Entity;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Web.UI.Models;
using Newtonsoft.Json;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Account
        public ActionResult Index()
        {
           return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success && ModelState.IsValid)
            {
                try
                {
                    user.IsActive = true;
                    user.LastActivityDate = DateTime.Now;
                    db.Users.Add(user);
                    db.SaveChanges();
                    Mail mail = new Mail();
                    mail.NewUserMail(user.Email,user.Name,user.Surname);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                TempData["Status"] = "Lütfen robot olmadığınızı belirtmek için testi geçiniz.";
                return RedirectToAction("Register", "Account");
            }
           
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success && ModelState.IsValid)
            {
                var user = db.Users.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
                if (user != null)
                {
                    HttpCookie cerez = new HttpCookie("cerezim");
                    cerez.Values.Add("eposta", user.Email);
                    cerez.Values.Add("ad", user.Name);
                    cerez.Values.Add("soyad", user.Surname);
                    cerez.Values.Add("ID", user.ID.ToString());
                    cerez.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cerez);
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Bilgilerinizi kontrol edin.");
                    return RedirectToAction("Login", "Account");
                }

                Session["ad"] = user.Name;
                Session["soyad"] = user.Surname;
                Session["ID"] = user.ID.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Status"] = "Lütfen robot olmadığınızı belirtmek için testi geçiniz.";
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Logout()
        {
            Session["eposta"] = null;
            Session["ID"] = null;
            Session["ad"] = null;
            Session["soyad"] = null;
            Session.Abandon();
            if (Request.Cookies["cerezim"] != null)
            {
                Response.Cookies["cerezim"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home");
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