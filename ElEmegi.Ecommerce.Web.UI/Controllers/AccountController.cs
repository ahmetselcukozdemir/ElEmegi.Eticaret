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
                    mail.NewUserMail(user.Email, user.Name, user.Surname);
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
        public ActionResult Login(string Email, string Password)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success && ModelState.IsValid)
            {
                var user = db.Users.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
                if (user != null)
                {
                    HttpCookie cerez = new HttpCookie("cerezim");
                    cerez.Values.Add("eposta", Server.UrlEncode(user.Email));
                    cerez.Values.Add("ad", Server.UrlEncode(user.Name));
                    cerez.Values.Add("soyad", Server.UrlEncode(user.Surname));
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

        public ActionResult ForgotMyPassword()
        {
            return View();
        }

        public ActionResult ResetPasswordMail(string email)
        {
            var user = db.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user != null)
            {
                char[] cr = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
                string result = string.Empty;
                Random r = new Random();
                for (int i = 0; i < 8; i++)
                {
                    result += cr[r.Next(0, cr.Length - 1)].ToString();
                }
                HttpCookie cerez = new HttpCookie("reset_password");
                cerez.Values.Add("code", Server.UrlEncode(result));
                cerez.Values.Add("reset_user_id",Server.UrlEncode(user.ID.ToString()));
                cerez.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cerez);

                Session["password_id"] = result;
                Mail mail = new Mail();
                mail.ForgotMyPassword(email, result);

                //RedirectToAction("NewPassword", "Account", new { user_id = user.ID });
            }
            return RedirectToAction("NewPassword", "Account", new { user_id = user.ID });
        }

        [HttpGet]
        public ActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(string password,string repassword, string reset_code)
        {
            var reset_cerez = Request.Cookies["reset_password"];
            var userid = reset_cerez["reset_user_id"];
            var data = db.Users.Where(x => x.ID.ToString() == userid).FirstOrDefault();
            var code = reset_cerez["code"];
            if (data != null && password == repassword)
            {
                if (code == reset_code)
                {
                    data.Password = password;
                    db.SaveChanges();
                    Response.Cookies["reset_password"].Expires = DateTime.Now.AddDays(-1);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Şifreleriniz uyuşmuyor ya da böyle bir kullanıcı bulunamadı.";
            }
            return View();
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