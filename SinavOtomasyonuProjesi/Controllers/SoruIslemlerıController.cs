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
    public class SoruIslemlerıController : Controller
    {
        //
        // GET: /SoruIslemlerı/
        string soruTuru = "";
        string H_id = "";
        public static string message = "";
        private static int postsPerPage = 5;
        public ActionResult Klasiklist(int page=1)
        {
           
            soruTuru = "Klasik";
            H_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(H_id);
            using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
            {

                var query = db.Sorular.ToList();
                var result = query.Where(x => x.H_id == hid).ToList();
                var acaba = result.Where(x => x.Sturu == soruTuru).OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
                int totalPostCount = result.Where(x => x.Sturu == soruTuru).Count();
                return View(new PageList()
                {
                    Sorularımm = new PageData<Sorular>(acaba, totalPostCount, page, postsPerPage)
                });

            }

        }


        public ActionResult Klasikedit(int id)
        {
            SınavProjesiEntities1 db = new SınavProjesiEntities1();

            return View("Klasikedit", db.Sorular.Find(id));
        }

        [HttpPost]
        public ActionResult Klasikedit(Sorular soru, int id)
        {
            soruTuru = "Klasik";
            try
            {
                using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
                {
                    Sorular eksisoru = db.Sorular.FirstOrDefault(x => x.Sid == id);
                    if (eksisoru != null)
                    {
                        if (db.Dersler.Where(x => x.Ders_Adi == soru.Ders).Count() != 0)
                        {
                            eksisoru.Smetni = soru.Smetni;
                            eksisoru.Spuan = soru.Spuan;
                            eksisoru.Sturu = soruTuru;
                            eksisoru.Ders = soru.Ders;
                        }
                        else
                        {
                            message = "Geçerli Bir Ders Giriniz !!";
                            ViewBag.message = message;
                            return View();
                        }

                        db.SaveChanges();
                        return RedirectToRoute("soru5");
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {

                return null;
            }
            return View();

        }


        public ActionResult Klasikdelete(int id)
        {
            try
            {
                using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
                {
                    Sorular soru = db.Sorular.FirstOrDefault(x => x.Sid == id);
                    if (soru != null)
                    {
                        db.Sorular.Remove(soru);
                        db.SaveChanges();
                        return RedirectToRoute("soru5");
                    }


                    else
                    {
                        return RedirectToRoute("soru5");
                    }


                }
            }
            catch (Exception e)
            {

                return null;
            }

            return View();
        }

        public ActionResult Testlist(int page=1)
        {
             soruTuru = "Test";
               H_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(H_id);

            SınavProjesiEntities1 con = new SınavProjesiEntities1();
            List<Soru> listSoru = new List<Soru>();

                var result = con.Sorular.Where(s => s.H_id == hid).ToList();
                var dblistSoruList = result.Where(x => x.Sturu == soruTuru).ToList();
                try
                {
                   
                        for (int i = 0; i < dblistSoruList.Count; i++)
                        {
                            Soru _soru = new Soru();
                            _soru.Sid = dblistSoruList.ElementAt(i).Sid;
                            _soru.Spuan = dblistSoruList.ElementAt(i).Spuan;
                            _soru.Smetni = dblistSoruList.ElementAt(i).Smetni;
                            _soru.H_id = hid;
                            _soru.Sturu = soruTuru;
                            _soru.Ders = dblistSoruList.ElementAt(i).Ders;

                            var dbSoruCevap = con.Cevaplar.Where(s => s.Sid == _soru.Sid).FirstOrDefault();

                            if (dbSoruCevap != null)
                            {
                                _soru.A = dbSoruCevap.A;
                                _soru.B = dbSoruCevap.B;
                                _soru.C = dbSoruCevap.C;
                                _soru.D = dbSoruCevap.D;
                                _soru.E = dbSoruCevap.E;
                                _soru.DogruCevap = dbSoruCevap.DogruCevap;
                            }

                            listSoru.Add(_soru);
                            
                          
                        }
                    }
                catch (Exception e)
                {
                    
                    throw;
                }
                var liste = listSoru.OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
                int totalPostCount = listSoru.Count();
                return View(new PageListV()
                {
                    Sorularımdr = new PageData<Soru>(liste, totalPostCount, page, postsPerPage)
                });
              
            
        }

      
        public ActionResult Testedit(int id)
        {
            soruTuru = "Test";
            SınavProjesiEntities1 db = new SınavProjesiEntities1();

            var Cevap = db.Cevaplar.Where(s => s.Sid == id).FirstOrDefault();
            var Soru = db.Sorular.Where(s => s.Sid == id).FirstOrDefault();
            Soru __soru = new Soru();
            __soru.Sid = id;
            __soru.Spuan = Soru.Spuan;
            __soru.Smetni = Soru.Smetni;
            __soru.Ders = Soru.Ders;
            __soru.Sturu = soruTuru;
            __soru.A = Cevap.A;
            __soru.B = Cevap.B;
            __soru.C = Cevap.C;
            __soru.D = Cevap.D;
            __soru.E = Cevap.E;
            __soru.DogruCevap = Cevap.DogruCevap;

            return View("Testedit", __soru);
        }

          [HttpPost]
        public ActionResult Testedit(Sorular soru, Cevaplar cevap, int id)
        {
            soruTuru = "Test";
            try
            {
                using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
                {
                    Sorular eksisoru = db.Sorular.FirstOrDefault(x => x.Sid == id);
                    Cevaplar eskicevap = db.Cevaplar.FirstOrDefault(x => x.Sid == id);
                    if (eksisoru != null)
                    {
                        if (eskicevap != null)
                        {


                            if (db.Dersler.Where(x => x.Ders_Adi == soru.Ders).Count() != 0)
                            {
                                eksisoru.Smetni = soru.Smetni;
                                eksisoru.Spuan = soru.Spuan;
                                eksisoru.Sturu = soruTuru;
                                eksisoru.Ders = soru.Ders;
                                eskicevap.A = cevap.A;
                                eskicevap.B = cevap.B;
                                eskicevap.C = cevap.C;
                                eskicevap.D = cevap.D;
                                eskicevap.E = cevap.E;
                                eskicevap.DogruCevap = cevap.DogruCevap;
                            }
                            else
                            {
                                message = "Geçerli Bir Ders Giriniz !!";
                                ViewBag.message = message;
                                return View();
                            }

                            db.SaveChanges();
                            return RedirectToRoute("soru8");
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {

                return null;
            }
            return View();
        }
        public ActionResult Testdelete(int id)
        {
            try
            {
                using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
                {
                    Sorular soru = db.Sorular.FirstOrDefault(x => x.Sid == id);
                    Cevaplar cevap = db.Cevaplar.FirstOrDefault(x => x.Sid == id);
                    if (soru != null)
                    {
                        db.Sorular.Remove(soru);
                        db.Cevaplar.Remove(cevap);
                        db.SaveChanges();
                        return RedirectToRoute("soru8");
                    }


                    else
                    {
                        return RedirectToRoute("soru8");
                    }


                }
            }
            catch (Exception e)
            {

                return null;
            }

            return View();
        }

        public ActionResult dogruyanlıslist(int page=1)
        {
            soruTuru = "Doğru_Yanlış";
            H_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(H_id);

            SınavProjesiEntities1 con = new SınavProjesiEntities1();
            List<Soru> listSoru = new List<Soru>();

            var result = con.Sorular.Where(s => s.H_id == hid).ToList();
            var dblistSoruList = result.Where(x => x.Sturu == soruTuru).ToList();
            try
            {

                for (int i = 0; i < dblistSoruList.Count; i++)
                {
                    Soru _soru = new Soru();
                    _soru.Sid = dblistSoruList.ElementAt(i).Sid;
                    _soru.Spuan = dblistSoruList.ElementAt(i).Spuan;
                    _soru.Smetni = dblistSoruList.ElementAt(i).Smetni;
                    _soru.H_id = hid;
                    _soru.Sturu = soruTuru;
                    _soru.Ders = dblistSoruList.ElementAt(i).Ders;

                    var dbSoruCevap = con.Cevaplar.Where(s => s.Sid == _soru.Sid).FirstOrDefault();

                    if (dbSoruCevap != null)
                    {
                        _soru.DogruCevap = dbSoruCevap.DogruCevap;
                    }

                    listSoru.Add(_soru);

                }
            }
            catch (Exception e)
            {

                throw;
            }

            var listeD = listSoru.OrderBy(x => x.Sid).Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
            int totalPostCount = listSoru.Count();
            return View(new PageListV()
            {
                Sorularımdr = new PageData<Soru>(listeD, totalPostCount, page, postsPerPage)
            });
         
        }

        public ActionResult dogruyanlısedit(int id)
        {
            soruTuru = "Doğru_Yanlış";
            SınavProjesiEntities1 db = new SınavProjesiEntities1();

            var Cevap = db.Cevaplar.Where(s => s.Sid == id).FirstOrDefault();
            var Soru = db.Sorular.Where(s => s.Sid == id).FirstOrDefault();
               Soru _soru = new Soru();
               _soru.Sid = id;
               _soru.Spuan = Soru.Spuan;
               _soru.Smetni = Soru.Smetni;
               _soru.Ders = Soru.Ders;
               _soru.Sturu = soruTuru;
               _soru.DogruCevap = Cevap.DogruCevap;
                
         
            return View("dogruyanlısedit",_soru);
        }


        [HttpPost]
        public ActionResult dogruyanlısedit(Sorular soru,Cevaplar cevap,int id)
        {
            soruTuru = "Doğru_Yanlış";
            try
            {
                using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
                {
                    Sorular eksisoru = db.Sorular.FirstOrDefault(x => x.Sid == id);
                    Cevaplar eskicevap = db.Cevaplar.FirstOrDefault(x => x.Sid == id);
                    if (eksisoru != null)
                    {
                        if (eskicevap!=null)
                        {
                            
                       
                        if (db.Dersler.Where(x => x.Ders_Adi == soru.Ders).Count() != 0)
                        {
                            eksisoru.Smetni = soru.Smetni;
                            eksisoru.Spuan = soru.Spuan;
                            eksisoru.Sturu = soruTuru;
                            eksisoru.Ders = soru.Ders;
                            eskicevap.DogruCevap = cevap.DogruCevap;
                        }
                        else
                        {
                            message = "Geçerli Bir Ders Giriniz !!";
                            ViewBag.message = message;
                            return View();
                        }

                        db.SaveChanges();
                        return RedirectToRoute("soru11");
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {

                return null;
            }
            return View();

        }
        public ActionResult dogruyanlısdelete(int id)
        {
            try
            {
                using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
                {
                    Sorular soru = db.Sorular.FirstOrDefault(x => x.Sid == id);
                    Cevaplar cevap = db.Cevaplar.FirstOrDefault(x => x.Sid == id);
                    if (soru != null)
                    {
                        db.Sorular.Remove(soru);
                        db.Cevaplar.Remove(cevap);
                        db.SaveChanges();
                        return RedirectToRoute("soru11");
                    }


                    else
                    {
                        return RedirectToRoute("soru11");
                    }


                }
            }
            catch (Exception e)
            {

                return null;
            }

            return View();
        }

    }
}


