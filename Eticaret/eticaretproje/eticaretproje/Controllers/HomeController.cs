using eticaretproje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eticaretproje.Controllers
{
    [UseAuthorize]
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            if (Request.Cookies["KullaniciAdi"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.KullaniciAdi = Request.Cookies["KullaniciAdi"].Value;
            }
            return View();
        }
    }
}