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
        public ActionResult SoruEkle(Sinavlar form, string Dersler)
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

        private SınavProjesiEntities1 db = new SınavProjesiEntities1();
        private static int postsPerPage = 5;

        public ActionResult SinavList(Sinavlar sinav, PageListV form, bool k=false, bool d = false, bool t = false, int page = 1)
        {
            if (k == false)
                k = form.klasik;
            if (d == false)
                d = form.dogru;
            if (t == false)
                t = form.test;
            sinav = _sinav;
            var soru = db.Sorular.Where(x => x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            int totalPostCount = 0;
            totalPostCount = db.Sorular.Where(x => x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();
            List<Soru> listSoru = new List<Soru>();
            //if (sorular[0] == "hepsi" || sorular == null)
            //{
            //    totalPostCount = db.Sorular.Where(x => x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();

            //}
            if (k == false && d == false && t == true)
            {
                totalPostCount = db.Sorular.Where(x => x.Sturu == "Test" && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();
                soru = db.Sorular.Where(x => x.Sturu == "Test" && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            }
            else if (k == true&&d==false&&t==false)
            {
                totalPostCount = db.Sorular.Where(x => x.Sturu == "Klasik" && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();
                soru = db.Sorular.Where(x => x.Sturu == "Klasik" && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            }
            else if (k == false && d == true && t == false)
            {
                totalPostCount = db.Sorular.Where(x => x.Sturu == "Doğru_Yanlış" && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();
                soru = db.Sorular.Where(x => x.Sturu == "Doğru_Yanlış" && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            }
            if (t == true && k == true&&d== false)
            {
                totalPostCount = db.Sorular.Where(x => (x.Sturu == "Klasik" || x.Sturu == "Test") && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();
                soru = db.Sorular.Where(x => (x.Sturu == "Klasik" || x.Sturu == "Test") && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            }
            else if (t == true && d == true&&k== false)
            {
                totalPostCount = db.Sorular.Where(x => (x.Sturu == "Doğru_Yanlış" || x.Sturu == "Test") && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();
                soru = db.Sorular.Where(x => (x.Sturu == "Doğru_Yanlış" || x.Sturu == "Test") && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            }
            else if (k == true && d == true&&t== false)
            {
                totalPostCount = db.Sorular.Where(x => (x.Sturu == "Doğru_Yanlış" || x.Sturu == "Klasik") && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).Count();
                soru = db.Sorular.Where(x => (x.Sturu == "Doğru_Yanlış" || x.Sturu == "Klasik") && x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            }
            

            //var soru = db.Sorular.Where(x => x.H_id == sinav.S_Hoca_id && x.Ders == sinav.DersAdi).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            foreach (var i in soru)
            {
                Soru _soru = new Soru();
                _soru.H_id = i.H_id;
                _soru.Sid = i.Sid;
                _soru.Spuan = i.Spuan;
                _soru.Smetni = i.Smetni;
                _soru.Sturu = i.Sturu;
                var cevap = db.Cevaplar.Where(x => x.Sid == i.Sid).SingleOrDefault();
                if (cevap != null)
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
                Sorularımdr = new PageData<Soru>(listSoru, totalPostCount, page, postsPerPage),
                test = t,
                klasik = k,
                dogru = d
            });

        }
    }
}


