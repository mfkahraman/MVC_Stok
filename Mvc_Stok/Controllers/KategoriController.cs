using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Mvc_Stok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Category
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.Tbl_Kategoriler.ToList();
            var degerler = db.Tbl_Kategoriler.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(Tbl_Kategoriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }

            db.Tbl_Kategoriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            db.Tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult KategoriGuncelle(Tbl_Kategoriler model)
        {
            var ktg = db.Tbl_Kategoriler.Find(model.KategoriID);
            ktg.KategoriAd = model.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}