using SinavOtomasyonuProjesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SinavOtomasyonuProjesi.ViewModels;

namespace SinavOtomasyonuProjesi.Controllers
{
    public class PostsController : Controller
    {
        [Authorize]
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");

        }

        [AllowAnonymous]
        public ActionResult Index()
        {
          
            return View();
        }

        public Hocalar get()
        {
            return hoca;
        }

        public static Hocalar hoca = new Hocalar();
        public static string message = "";

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Hocalar c,string ReturnUrl)
        {
          
            
            if (ModelState.IsValid)
            {

                using (SınavProjesiEntities1 con = new SınavProjesiEntities1())
                {
                     hoca = con.Hocalar.Where(s => s.HocaAdı.Equals(c.HocaAdı) && s.Şifresi.Equals(c.Şifresi)).FirstOrDefault();
                     if (hoca != null)
                     {
                         FormsAuthentication.SetAuthCookie(c.HocaAdı, true);
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                          
                            Session["HocaAdı"] = hoca.HocaAdı.ToString();
                            Session["Şifresi"] = hoca.Şifresi.ToString();
                            Session["hid"] = hoca.HocaId.ToString();

                            return RedirectToRoute("soru3");
                        }
                       
                    }
                    else
                     {
                         message = "Geçerli Bir Kullanıcı Adı veya Şifre Giriniz!!";
                         ViewBag.message = message;
                         return View();

                    }
                }
              
            }

            ModelState.Remove("Şifresi");
            return View();
        }


        public ActionResult CreateUser()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Hocalar form)
        {
         
            if (ModelState.IsValid)
            {
               
                try
                {
                    SınavProjesiEntities1 db = new SınavProjesiEntities1();
                        Hocalar user = new Hocalar();
                    
                        if (db.Hocalar.Where(x => x.Email == form.Email).Count() == 0)
                        {
                            user.HocaAdı = form.HocaAdı.Trim();
                            user.HocaSoyadı = form.HocaSoyadı.Trim();
                            user.Şifresi = form.Şifresi.Trim();
                            user.Ünvanı = form.Ünvanı.Trim();
                            user.Email = form.Email.Trim();

                            db.Hocalar.Add(user);
                            db.SaveChanges();
                            return RedirectToRoute("Home");
                        }
                        else
                        {
                            message = "Geçerli Bir Email Adresi Giriniz!!";
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

        public ActionResult SifremiUnuttum()
        {

            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SifremiUnuttum(Hocalar c)
        {
           
            if (ModelState.IsValid)
            {
                using (SınavProjesiEntities1 con = new SınavProjesiEntities1())
                {
                      var count = con.Hocalar.Where(s => s.Email.Equals(c.Email)).FirstOrDefault();
                    if (count != null)
                    {
                        SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                        var mail = new MailMessage();
                        mail.IsBodyHtml = true;
                        mail.From = new MailAddress("SinavOtomasyonu@hotmail.com");
                        mail.To.Add(c.Email);
                        mail.Subject = "Kullanıcı Bilgileri Hatırlatma";

                        string htmlBody;

                        htmlBody = "Şifreniz:" + count.Şifresi.ToString() + "<br>Giriş İçin <a href='#'>Tıklayınız</a>";
                        mail.Body = htmlBody;

                        SmtpServer.Port = 587;
                        SmtpServer.UseDefaultCredentials = false;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("SinavOtomasyonu@hotmail.com", "CTRL123alt");
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);
                        message = "Şifreniz Email Adresinize gönderilmiştir.";
                        ViewBag.message = message;
                        return View();

                    }
                    else
                    {
                        message = "Sitemizde Bu Email Adresi Kayıtlı Değil, Lütfen geçerli bir Email adresi giriniz";
                        ViewBag.message = message;
                        return View();
                    }
                 


                }

            }
            return View();


        }
      
    }
}


