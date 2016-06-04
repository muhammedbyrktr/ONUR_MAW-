using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SinavOtomasyonuProjesi.ViewModels;
using SinavOtomasyonuProjesi.Models;
using System.Web.Security;
//using System.Data.Entity.Core.Objects;

namespace SinavOtomasyonuProjesi.Controllers
{
    public class SoruController : Controller
    {
        //
        // GET: /Soru/ 
       static string soruTuru = "";
        static string H_id = "";
       
    
        public static string message = "";
        public ActionResult Klasik()
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

        [HttpPost]
        public ActionResult Klasik(Soru klasik, string Dersler)
        {

            H_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(H_id);
            SınavProjesiEntities1 db = new SınavProjesiEntities1();
            var derslist = db.Dersler.Where(s => s.Hoca_id == hid).Select(x => new SelectListItem
            {
                Text = x.Ders_Adi,
                Value = x.Ders_Adi
            });

            ViewBag.Dersler = derslist;
            ViewData["Dersler"] = derslist;

            soruTuru = "Klasik";
            ViewBag.Ders = Dersler;

        
            Sorular _soru = new Sorular();
         
            if (db.Dersler.Where(x => x.Ders_Adi == Dersler).Count() != 0)
            {
                _soru.Smetni = klasik.Smetni.Trim();
                _soru.Sturu = soruTuru;
                _soru.H_id = hid;
                _soru.Spuan = Convert.ToInt32(klasik.Spuan);
                _soru.Ders = ViewBag.Ders = Dersler;
                
         
            }
            else
            {
                message = "Geçerli Bir Ders Giriniz !!";
                ViewBag.message = message;
                return View();
            }
           
            try
            {
                db.Sorular.Add(_soru);
                db.SaveChanges();
                ModelState.Remove("Smetni");
                ModelState.Remove("Spuan");
                ModelState.Remove("Dersler");
              
            }
            catch (Exception ex)
            {


            }
            message = "Klasik Soru Başarıyla Eklendi";
            ViewBag.message = message;
            return View();
        }
     
 

     
        public ActionResult Test()
        {
            H_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(H_id);
            SınavProjesiEntities1 db = new SınavProjesiEntities1();
            var derslist = db.Dersler.Where(s => s.Hoca_id == hid).Select(x => new SelectListItem
            {
                Text = x.Ders_Adi,
                Value = x.Ders_Adi
            });

            ViewBag.Dersler = derslist;
            ViewData["Dersler"] = derslist;

            var listsiklar = new SelectListItem[] {
                               new SelectListItem(){Text="A",Value="A"},
                                new SelectListItem(){Text="B",Value="B"},
                                 new SelectListItem(){Text="C",Value="C"},
                                  new SelectListItem(){Text="D",Value="D"},
                                   new SelectListItem(){Text="E",Value="E"},                  
            };

            ViewBag.Siklar = listsiklar;
            ViewData["Siklar"] = listsiklar;
            return View();
        }

        [HttpPost]
        public ActionResult Test(Soru test, string Siklar, string Dersler)
        {
            H_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(H_id);
            SınavProjesiEntities1 db = new SınavProjesiEntities1();
            var derslist = db.Dersler.Where(s => s.Hoca_id == hid).Select(x => new SelectListItem
            {
                Text = x.Ders_Adi,
                Value = x.Ders_Adi
            });

            ViewBag.Dersler = derslist;
            ViewData["Dersler"] = derslist;

            var listsiklar = new SelectListItem[] {
                               new SelectListItem(){Text="A",Value="A"},
                                new SelectListItem(){Text="B",Value="B"},
                                 new SelectListItem(){Text="C",Value="C"},
                                  new SelectListItem(){Text="D",Value="D"},
                                   new SelectListItem(){Text="E",Value="E"},                  
            };

            ViewBag.Siklar = listsiklar;
            ViewData["Siklar"] = listsiklar;

            soruTuru = "Test";
           

            Sorular _soru = new Sorular();
            Cevaplar _cevap = new Cevaplar();
            if (db.Dersler.Where(x => x.Ders_Adi == Dersler).Count() != 0)
            {
                    _soru.Smetni = test.Smetni.Trim();
                    _soru.Sturu = soruTuru;
                    _soru.H_id = hid;
                    _soru.Spuan = Convert.ToInt32(test.Spuan);
                    _soru.Ders = ViewBag.Ders = Dersler;

                    _cevap.A = test.A.Trim();
                    _cevap.B = test.B.Trim();
                    _cevap.C = test.C.Trim();
                    _cevap.D = test.D.Trim();
                    _cevap.E = test.E.Trim();
                    _cevap.DogruCevap =Siklar;
               
                
            }

            else
            {
                message = "Geçerli Bir Ders Giriniz !!";
                ViewBag.message = message;
                return View();
            }
           
            try
            {
                db.Sorular.Add(_soru);
                db.SaveChanges();

                int s_id = _soru.Sid;
                _cevap.Sid = s_id;
                db.Cevaplar.Add(_cevap);
                db.SaveChanges();
                ModelState.Remove("Smetni");
                ModelState.Remove("Spuan");
                ModelState.Remove("Dersler");
                ModelState.Remove("Siklar");
                ModelState.Remove("A");
                ModelState.Remove("B");
                ModelState.Remove("C");
                ModelState.Remove("D");
                ModelState.Remove("E");
            }
            catch (Exception ex)
            {


            }
            message = "Test Sorusu Başarıyla Eklendi";
            ViewBag.message = message;
            return View();
        }


        public ActionResult DogruYanlis()
        {
            H_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(H_id);
            SınavProjesiEntities1 db = new SınavProjesiEntities1();
            var derslist = db.Dersler.Where(s => s.Hoca_id == hid).Select(x => new SelectListItem
            {
                Text = x.Ders_Adi,
                Value = x.Ders_Adi
            });

            ViewBag.Dersler = derslist;
            ViewData["Dersler"] = derslist;

                var listsiklar = new SelectListItem[] {
                               new SelectListItem(){Text="Doğru",Value="Doğru"},
                                new SelectListItem(){Text="Yanlış",Value="Yanlış"},                
            };

                ViewBag.Doğru_Yanlış = listsiklar;
                ViewData["Doğru_Yanlış"] = listsiklar;

          
            return View();
        }

        [HttpPost]
        public ActionResult DogruYanlis(Soru dogruyanlis, string Doğru_Yanlış, string Dersler)
        {
            H_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(H_id);
            SınavProjesiEntities1 db = new SınavProjesiEntities1();
            var derslist = db.Dersler.Where(s => s.Hoca_id == hid).Select(x => new SelectListItem
            {
                Text = x.Ders_Adi,
                Value = x.Ders_Adi
            });

            ViewBag.Dersler = derslist;
            ViewData["Dersler"] = derslist;

            var listsiklar = new SelectListItem[] {
                               new SelectListItem(){Text="Doğru",Value="Doğru"},
                                new SelectListItem(){Text="Yanlış",Value="Yanlış"},                
            };

            ViewBag.Doğru_Yanlış = listsiklar;
            ViewData["Doğru_Yanlış"] = listsiklar;


            soruTuru = "Doğru_Yanlış";
            Sorular _soru = new Sorular();
            Cevaplar _cevap = new Cevaplar();

            if (db.Dersler.Where(x => x.Ders_Adi == Dersler).Count() != 0)
            {
                _soru.Smetni = dogruyanlis.Smetni.Trim();
                _soru.Sturu = soruTuru;
                _soru.H_id = hid;
                _soru.Spuan = Convert.ToInt32(dogruyanlis.Spuan);
                _soru.Ders = ViewBag.Ders = Dersler;
                _cevap.DogruCevap = Doğru_Yanlış;
            }
            else
            {
                message = "Geçerli Bir Ders Giriniz !!";
                ViewBag.message = message;
                return View();
            }
            try
            {
                db.Sorular.Add(_soru);
                db.SaveChanges();

                int s_id = _soru.Sid;
                _cevap.Sid = s_id;
                db.Cevaplar.Add(_cevap);
                db.SaveChanges();
                ModelState.Remove("Smetni");
                ModelState.Remove("Spuan");
                ModelState.Remove("Dersler");
                ModelState.Remove("Doğru_Yanlış");
               
            }
            catch (Exception ex)
            {


            }

            message = "Doğru-Yanlış Sorusu Başarıyla Eklendi";
            ViewBag.message = message;
            return View();
        }
       
        public ActionResult Profilim(FormCollection p)
        {
            Hocalar hoca = new Hocalar();
         
                if (hocal.Email == null)
                {


                    PostsController sa = new PostsController();
                    hoca = sa.get();
                    return View(hoca);
                }
                else
                {
                    message = "Bilgileriniz Başarıyla Güncellendi.";
                    ViewBag.message = message;
                    return View(hocal);
                }
                
          
        }

        public static Hocalar hocal = new Hocalar();
      
       
        public ActionResult Düzenle(Hocalar form)
        {
           
            Hocalar hoca = new Hocalar();
            PostsController sa = new PostsController();
            hoca = sa.get();
          
           
                if (form.HocaAdı != null)
                {

                    if (hoca != form)
                    {
                        SınavProjesiEntities1 con = new SınavProjesiEntities1();
                        hocal = con.Hocalar.Where(s => s.HocaId == hoca.HocaId).FirstOrDefault();
                        try
                        {
                           
                            if (con.Hocalar.Where(x => x.Email == form.Email).Count() == 0||form.Email==hoca.Email)
                            {
                                hocal.Email = form.Email;
                                hocal.HocaAdı = form.HocaAdı;
                                hocal.HocaSoyadı = form.HocaSoyadı;
                                hocal.Şifresi = form.Şifresi;
                                hocal.Ünvanı = form.Ünvanı;
                                //con.Hocalar.Remove();    

                                con.SaveChanges();
                                return RedirectToRoute("soru3");
                             }
                            else
                            {
                                message = "Geçerli Bir Email Adresi Giriniz!!";
                                ViewBag.message = message;
                                return View(form);
                            }
                        }
                        catch (Exception e)
                        {
                            
                            
                        }
                       

                    }
                    else
                    {
                       
                        return View(form);

                    }
                }
          
           
           
            return View(hoca);
        }
    }
}
