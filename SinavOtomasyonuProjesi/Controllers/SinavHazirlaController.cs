using SinavOtomasyonuProjesi.Infrastructure;
using SinavOtomasyonuProjesi.Models;
using SinavOtomasyonuProjesi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinavOtomasyonuProjesi.Controllers
{
    public class SinavHazirlaController : Controller
    {
        string H_id = "";
        public static string message = "";
        public ActionResult SoruEkle()
        {
            H_id = Session["hid"].ToString();
            int hid = Convert.ToInt32(H_id);
            SınavProjesiEntities1 db = new SınavProjesiEntities1();
            var derslist = db.Dersler.Where(s => s.Hoca_id == hid).Select(x => new SelectListItem
            {
                Text = x.Ders_Adi,
                Value = x.Ders_Adi
            });


            ViewBag.Dersler = derslist;
            ViewData["Dersler"] = derslist;
            return View();
        }
       static Sinavlar _sinav = new Sinavlar();
        [HttpPost]
        public ActionResult SoruEkle(Sinavlar form,string Dersler)
        {
            H_id = Session["hid"].ToString();
            int hid = Convert.ToInt32(H_id);
            SınavProjesiEntities1 db = new SınavProjesiEntities1();
            var derslist = db.Dersler.Where(s => s.Hoca_id == hid).Select(x => new SelectListItem
            {
                Text = x.Ders_Adi,
                Value = x.Ders_Adi
            });


            ViewBag.Dersler = derslist;
            ViewData["Dersler"] = derslist;
            ViewBag.Ders = Dersler;
             
            _sinav.S_Hoca_id = hid;
            _sinav.SınavTipi = form.SınavTipi.Trim();
            _sinav.SınavTarihi = form.SınavTarihi;
            _sinav.DersAdi = ViewBag.Ders = Dersler;
            //db.Sinavlar.Add(_sinav);
            //db.SaveChanges();


        return RedirectToRoute("SinavList");
        }

         private   SınavProjesiEntities1 db = new SınavProjesiEntities1();
        private static int postsPerPage = 5;

        public ActionResult SinavList(Sinavlar sinav, int page = 1)
        {
            sinav = _sinav;
            List<Soru> listSoru = new List<Soru>();
         
            int totalPostCount = db.Sorular.Where(x => x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();
            var soru=  db.Sorular.Where(x => x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x=>x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
             foreach(var i in soru)
            {
                Soru _soru = new Soru();
                _soru.H_id = i.H_id;
                _soru.Sid = i.Sid;
                _soru.Spuan = i.Spuan;
                _soru.Smetni = i.Smetni;
                _soru.Sturu = i.Sturu;
                var cevap = db.Cevaplar.Where(x => x.Sid == i.Sid).SingleOrDefault();
                if(cevap != null)
                {
                    _soru.A = cevap.A;
                    _soru.B = cevap.B;
                    _soru.C = cevap.C;
                    _soru.D = cevap.D;
                    _soru.E = cevap.E;
                    _soru.DogruCevap = cevap.DogruCevap;
                }
                listSoru.Add(_soru);
            }

            return View(new PageListV()
            {
                Sorularımdr = new PageData<Soru>(listSoru, totalPostCount, page, postsPerPage)
            });         
        }
    }
}


