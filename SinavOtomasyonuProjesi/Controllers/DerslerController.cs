using SinavOtomasyonuProjesi.Models;
using SinavOtomasyonuProjesi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinavOtomasyonuProjesi.Controllers
{
    public class DerslerController : Controller
    {
        //
        // GET: /Dersler/
        public ActionResult DersEkle()
        {
            return View();
        }

        string Hoca_id = "";
        public static string message = "";
        [HttpPost]
        public ActionResult DersEkle(Dersler form)
        {
            if (ModelState.IsValid)
            {
                Hoca_id = Session["Hid"].ToString();

                try
                {
                    SınavProjesiEntities1 db = new SınavProjesiEntities1();
                    Dersler ders = new Dersler();
                    if (db.Dersler.Where(x => x.Ders_Adi == form.Ders_Adi).Count() == 0)
                    {
                        ders.Ders_Adi = form.Ders_Adi.Trim();
                        ders.Hoca_id = Convert.ToInt32(Hoca_id);

                        db.Dersler.Add(ders);
                        db.SaveChanges();

                        message = "Ders Başarıyla Eklendi";
                        ViewBag.message = message;
                        ModelState.Remove("Ders_Adi");
                        return View();
                    }
                    else
                    {
                        message = "Ders Adı Zaten Var Farklı Bir Ders Giriniz!!";
                        ViewBag.message = message;
                        return View();

                    }
                }
                catch (Exception ex)
                {

                }

            }

            return View();

        }

        public ActionResult DersListele()
        {
            Hoca_id = Session["Hid"].ToString();
            int hid = Convert.ToInt32(Hoca_id);
            using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
            {
                //var context = new SinavOtomasyonuEntities1();
                var query = db.Dersler.ToList();
                var result = query.Where(x => x.Hoca_id == hid).ToList();

                return View(result);
            }
        }
        public ActionResult DersDüzenle(int id)
        {
            SınavProjesiEntities1 db = new SınavProjesiEntities1();

            return View("DersDüzenle", db.Dersler.Find(id));

        }

        [HttpPost]
        public ActionResult DersDüzenle(Dersler ders, int id)
        {
            try
            {
                using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
                {
                    Dersler eksiders = db.Dersler.FirstOrDefault(x => x.D_id == id);
                    if (eksiders != null)
                    {
                        if (db.Dersler.Where(x => x.Ders_Adi == ders.Ders_Adi).Count() == 0)
                        {
                            eksiders.Ders_Adi = ders.Ders_Adi;
                            db.SaveChanges();
                            return RedirectToRoute("Ders1");
                        }
                        else
                        {
                            message = "Ders Adı Zaten Var Farklı Bir Ders Giriniz!!";
                            ViewBag.message = message;
                            return View();
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

        public ActionResult DersSil(int id)
        {
            try
            {
                using (SınavProjesiEntities1 db = new SınavProjesiEntities1())
                {
                    Dersler ders = db.Dersler.FirstOrDefault(x => x.D_id == id);
                    if (ders != null)
                    {
                        db.Dersler.Remove(ders);
                        db.SaveChanges();
                        return RedirectToRoute("Ders1");
                    }


                    else
                    {
                        return RedirectToRoute("Ders1");
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

