using eticaretproje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eticaretproje.Controllers
{
    public class LoginController : Controller
    {
        eticaretprojesiEntities db = new eticaretprojesiEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login model)
        {
            Login giris = db.Login .Where(x => x.KullaniciAdi == model.KullaniciAdi && x.Sifre == model.Sifre).FirstOrDefault();
            if(giris==null)
            {
                ViewBag.mesaj ="Kullanıcı Adı Veya Şifreniz Yanlış";
                return View("Index");
            }
            else
            {
                if (giris.Adminmi==true)
                {
                    FormsAuthentication.SetAuthCookie(giris.KullaniciAdi, true);
                    HttpCookie cerez = new HttpCookie("KullaniciAdi", giris.KullaniciAdi);
                    cerez.Expires.AddDays(10);
                    Response.Cookies.Add(cerez);

                    return RedirectToAction("Index","Home");
                }
                else 
                {
                    TempData["sonuc"] = 0;
                    return View("Index");
                }
            }
        
        }

        public ActionResult Cikisyap()
        {
            Response.Cookies["KullaniciAdi"].Expires = DateTime.Now.AddDays(-10);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }


    }
}