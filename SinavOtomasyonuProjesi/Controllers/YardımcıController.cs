using SinavOtomasyonuProjesi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using SinavOtomasyonuProjesi.Models;
namespace SinavOtomasyonuProjesi.Controllers
{
    public class YardımcıController : Controller
    {
        //
        // GET: /Yardım/
         public static string message = "";
        public ActionResult Yardım()
        {
            return View();
        }
        public ActionResult Gizlilik_İlkeleri()
        {
            return View();
        }
         public ActionResult Koşullar()
        {
            return View();
        }
         public ActionResult İletişim()
         {
             return View();
         }

         [HttpPost]
         public ActionResult İletişim(EmailForm form,Hocalar a)
         {
             if (ModelState.IsValid)
             {

                 try
                 {
                     SınavProjesiEntities1 db = new SınavProjesiEntities1();
                     Hocalar user = new Hocalar();

                     if (db.Hocalar.Where(x => x.Email == a.Email).Count() == 0)
                     {

                         message = "Geçerli Bir Email Adresi Giriniz!!";
                         ViewBag.message = message;
                         return View();
                     }
                     else
                     {
                        

                         SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                         var mail = new MailMessage();
                         mail.From = new MailAddress("SinavOtomasyonu@hotmail.com");
                         mail.To.Add(form.Email);
                         mail.Subject = form.Konu;
                         mail.IsBodyHtml = true;
                         string htmlBody;
                         htmlBody = " İsim:   " + form.İsim + "<br>"+ "  Telefon: "  + form.Tel+"<br>"+"Mesaj:    " + form.Messaj ;
                         mail.Body = htmlBody;

                         SmtpServer.Port = 587;
                         SmtpServer.UseDefaultCredentials = false;
                         SmtpServer.Credentials = new System.Net.NetworkCredential("SinavOtomasyonu@hotmail.com", "CTRL123alt");
                         SmtpServer.EnableSsl = true;
                         SmtpServer.Send(mail);
                         message = "Mesajınız iletildi!!";
                         ViewBag.message = message;

                         return RedirectToAction("İletişim", "Yardımcı");
                         

                     }
                 }
                 catch (Exception ex)
                 {

                 }
             }
             return View();

         }
        

    }
}
