using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity;

namespace Mvc_Stok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.Tbl_Musteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.Tbl_Musteriler.ToList();
            //return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(Tbl_Musteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }

            db.Tbl_Musteriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSil(int id)
        {
            var musteri = db.Tbl_Musteriler.Find(id);
            db.Tbl_Musteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.Tbl_Musteriler.Find(id);
            return View(musteri);
        }

        public ActionResult MusteriGuncelle(Tbl_Musteriler model)
        {
            var musteri = db.Tbl_Musteriler.Find(model.MusteriID);
            musteri.MusteriAd = model.MusteriAd;
            musteri.MusteriSoyad = model.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}