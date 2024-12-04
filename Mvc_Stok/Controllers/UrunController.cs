using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity;

namespace Mvc_Stok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.Tbl_Urunler.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Tbl_Urunler p1)
        {
            var ktg = db.Tbl_Kategoriler.Where(i => i.KategoriID == p1.Tbl_Kategoriler.KategoriID).FirstOrDefault();
            p1.Tbl_Kategoriler = ktg;
            db.Tbl_Urunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kategoriCombobox = (from i in db.Tbl_Kategoriler.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.KategoriAd,
                                                         Value = i.KategoriID.ToString(),
                                                     }).ToList();
            ViewBag.ktgCmb = kategoriCombobox;
            var urun = db.Tbl_Urunler.Find(id);
            return View("UrunGetir",urun);
        }

        public ActionResult UrunGuncelle(Tbl_Urunler model)
        {
            var urun = db.Tbl_Urunler.Find(model.UrunId);
            urun.UrunAd = model.UrunAd;
            urun.Marka = model.Marka;
            var ktg = db.Tbl_Kategoriler.Where(m=> m.KategoriID == model.Tbl_Kategoriler.KategoriID).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriID;
            urun.Fiyat = model.Fiyat;
            urun.Stok = model.Stok;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}