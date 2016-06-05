using SinavOtomasyonuProjesi.Infrastructure;
using SinavOtomasyonuProjesi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SinavOtomasyonuProjesi.ViewModels
{
    public class sinavkagıdı { 
    public IEnumerable<SinavKagıdı> Sinavkagıdım { get; set; }
    }
    public class Soru
    {
        public int Sid { get; set; }
        public string Sturu { get; set; }
        public string Smetni { get; set; }
        public string Ders { get; set; }
        public int H_id { get; set; }
        public Nullable<int> Spuan { get; set; }



        public int Cid { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string DogruCevap { get; set; }

        public IEnumerable<SelectListItem> Dersler { get; set; }
        public IEnumerable<SelectListItem> Siklar { get; set; }
        public IEnumerable<SelectListItem> Doğru_Yanlış { get; set; }
        public string message { get; set; }

    }

    public class PageList
    {
        public PageData<Sorular> Sorularımm { get; set; }
    }

    public class PageListV
    {
        public PageData<Soru> Sorularımdr { get; set; }
        public bool test { get; set; }
        public bool klasik { get; set; }
        public bool dogru { get; set; }
        public bool Durum { get; set; }
    }
}