using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();
        [HttpGet]
        // GET: Admin
        public ActionResult Index()
        {
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

        public ActionResult Logout()
        {
            if (Request.Cookies["admin_cerezim"] != null)
            {
                Response.Cookies["admin_cerezim"].Expires = DateTime.Now.AddHours(-1);
            }
            return RedirectToAction("Login", "Admin");
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
            if (id !=null && member !=null)
            {
                member.Email = entity.Email;
                member.Name = entity.Name;
                member.Surname = entity.Surname;
                member.Photo = entity.Photo;
                member.Phone = entity.Phone;
                member.Password = entity.Password;
                if (image.FileName != null)
                {
                    if (image.ContentLength > 0)
                    {
                        double FileSize = Convert.ToDouble(image.ContentLength / 1024);
                        if (FileSize > 10240)
                        {
                            ViewBag.SizeError = "Fotografınız 10 mb'dan büyük olamaz.";
                        }
                        string image_name = System.IO.Path.GetFileName(image.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/images/profiles/" + image_name));
                        image.SaveAs(path);
                        member.Photo = image_name;
                    }
                }
                db.SaveChanges();
                ViewBag.SuccessMessage = "Bilgileriniz başarıyla güncellenmiştir.";
            }
            RedirectToAction("MemberInformation").ExecuteResult(this.ControllerContext);
        }
    }
}