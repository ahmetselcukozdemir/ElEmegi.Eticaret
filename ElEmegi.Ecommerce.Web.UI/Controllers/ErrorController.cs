using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElEmegi.Ecommerce.Web.UI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageError()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Page404()
        {
            ViewBag.ErrorMessage = "Aradığınız sayfa bulunamıyor.Yetkiliniz ile paylaşıldı, merak etmeyin :)";
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            return View("PageError");
        }
        public ActionResult Page403()
        {
            ViewBag.ErrorMessage = "Bu sayfaya erişmek için yetkiniz bulunmamaktadır.";
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 403;
            return View("PageError");
        }
        public ActionResult Page500()
        {
            ViewBag.ErrorMessage = " Internal Server hatası karşılaşıldı.";
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
    }
}