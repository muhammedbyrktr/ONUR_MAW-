using SinavOtomasyonuProjesi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SinavOtomasyonuProjesi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(PostsController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" }, namespaces);
            //routes.MapRoute("Login", "login", new { controller = "Auth", action = "login" }, namespaces);   
            routes.MapRoute("logout", "logout", new { controller = "Posts", action = "logout" }, namespaces);
            routes.MapRoute("kaydet", "kaydet", new { controller = "Posts", action = "CreateUser" }, namespaces);
            routes.MapRoute("unuttum", "unuttum", new { controller = "Posts", action = "SifremiUnuttum" }, namespaces);

            routes.MapRoute("soru", "soru", new { controller = "Soru", action = "Klasik" }, namespaces);
            routes.MapRoute("soru1", "soru1", new { controller = "Soru", action = "Test" }, namespaces);
            routes.MapRoute("soru2", "soru2", new { controller = "Soru", action = "DogruYanlis" }, namespaces);
            routes.MapRoute("soru3", "soru3", new { controller = "Soru", action = "Profilim" }, namespaces);
            routes.MapRoute("soru4", "soru4", new { controller = "Soru", action = "Düzenle" }, namespaces);

            routes.MapRoute("soru5", "soru5", new { controller = "SoruIslemlerı", action = "Klasiklist" }, namespaces);
            routes.MapRoute("soru6", "soru6", new { controller = "SoruIslemlerı", action = "Klasikedit" }, namespaces);
            routes.MapRoute("soru7", "soru7", new { controller = "SoruIslemlerı", action = "Klasikdelete" }, namespaces);

            routes.MapRoute("soru8", "soru8", new { controller = "SoruIslemlerı", action = "Testlist" }, namespaces);
            routes.MapRoute("soru9", "soru9", new { controller = "SoruIslemlerı", action = "Testedit" }, namespaces);
            routes.MapRoute("soru10", "soru10", new { controller = "SoruIslemlerı", action = "Testdelete" }, namespaces);

            routes.MapRoute("soru11", "soru11", new { controller = "SoruIslemlerı", action = "dogruyanlıslist" }, namespaces);
            routes.MapRoute("soru12", "soru12", new { controller = "SoruIslemlerı", action = "dogruyanlısedit" }, namespaces);
            routes.MapRoute("soru13", "soru13", new { controller = "SoruIslemlerı", action = "dogruyanlısdelete" }, namespaces);

            routes.MapRoute("Ders", "Ders", new { controller = "Dersler", action = "DersEkle" }, namespaces);
            routes.MapRoute("Ders1", "Ders1", new { controller = "Dersler", action = "DersListele" }, namespaces);
            routes.MapRoute("Ders2", "Ders2", new { controller = "Dersler", action = "DersDüzenle" }, namespaces);
            routes.MapRoute("Ders3", "Ders3", new { controller = "Dersler", action = "DersSil" }, namespaces);
            
            // Yardım Controller

            routes.MapRoute("Yardım", "Yardım", new { controller = "Yardımcı", action = "Yardım" }, namespaces);
            routes.MapRoute("Gizlilik İlkeleri", "Gizlilik İlkeleri", new { controller = "Yardımcı", action = "Gizlilik_İlkeleri" }, namespaces);
            routes.MapRoute("Kullanım Koşulları", "Kullanım Koşulları", new { controller = "Yardımcı", action = "Koşullar" }, namespaces);
            routes.MapRoute("İletişim", "İletişim", new { controller = "Yardımcı", action = "İletişim" }, namespaces);

            routes.MapRoute("SoruEkle", "SoruEkle", new { controller = "SinavHazirla", action = "SoruEkle" }, namespaces);
            routes.MapRoute("SinavList", "SinavList", new { controller = "SinavHazirla", action = "SinavList" }, namespaces);
            routes.MapRoute("SinavKagıdı", "SinavKagıdı", new { controller = "SinavHazirla", action = "SinavKagıdı" }, namespaces);
        }
    }
}