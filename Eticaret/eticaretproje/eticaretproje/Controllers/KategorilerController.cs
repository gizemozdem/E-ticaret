using eticaretproje.Models;
using System;
using System.Data.Entity.Core.Common;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;

namespace eticaretproje.Controllers
{
    public class KategorilerController : Controller
    {

        eticaretprojesiEntities db = new eticaretprojesiEntities();
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kategoriler model)
        {
            try
            {
                var kategori = db.Kategoriler.Add(model);
                kategori.UstkategoriId = 0;
                int sonuc = db.SaveChanges();
                if (sonuc == 1)
                {
                    TempData["sonuc"] = "1";

                }
                else
                {

                    TempData["sonuc"] = "0";
                }
            }
            catch (Exception)
            {
                return View("Ekle");
            }

            return View("Ekle");
        }
        public ActionResult Liste()
        {
            try
            {
                var liste = db.Kategoriler.ToList();
                return View(liste);
            }
            catch (Exception)
            {
                return View("Ekle");
            }



        }

        public PartialViewResult kategoriliste()
        {
            var liste = db.Kategoriler.ToList();
            return PartialView(liste);
           
        }

        public ActionResult KategoriSil(int id)
        {
            try
            {
                var silinecek = db.Kategoriler.Find(id);
                db.Kategoriler.Remove(silinecek);
                int sonuc = db.SaveChanges();
                if(sonuc==1)
                {
                    TempData["sonuc"] = 1;
                }
                else
                {
                    TempData["sonuc"] = 0;
                }
                return RedirectToAction("Liste");

            }
            catch(Exception)
            {
                return RedirectToAction("Liste");

            }

        }

        public ActionResult Guncelle(int id)
        {
            try
            {
                if(id==null)
                {
                    return RedirectToAction("Liste");
                }

                var guncellenecekkategori = db.Kategoriler.Where(x => x.Id == id).FirstOrDefault();
                if(guncellenecekkategori ==null)
                {
                    TempData["sonuc"] = 0;
                    return RedirectToAction("Guncelle");
                }
                
            }
            catch(Exception)
            {
                return RedirectToAction("Liste");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Guncelle(Kategoriler model)
        {
            try
            {
                Kategoriler guncellenecek = db.Kategoriler.FirstOrDefault(x => x.Id == model.Id);
                guncellenecek.Tanim = model.Tanim;
                int sonuc = db.SaveChanges();
                if(sonuc==1)
                {
                    TempData["sonuc"] = 1;
                }
                else
                {
                    TempData["sonuc"] = 0;
                }
                return RedirectToAction("Liste");
            }
            catch(Exception)
            {
                return RedirectToAction("Guncelle");
            }
            return View();
        }
        public ActionResult altkategori()
        {
            var kategori = db.Kategoriler.ToList().Select(x => new SelectListItem
            {
                Selected = false,
                Text = x.Tanim,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.kategori = kategori;
            return View();
        }
        [HttpPost]
        public ActionResult altkategori(Kategoriler model)
        {
            try
            {
                var eklenecekkategori = db.Kategoriler.Add(model);
                eklenecekkategori.UstkategoriId = model.Id;
                db.SaveChanges();
                return View("Ekle");
            }
            catch(Exception)
            {
                return View("Ekle");
            }

        }
    }

}