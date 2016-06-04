using SinavOtomasyonuProjesi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SinavOtomasyonuProjesi.ViewModels
{
    public class AuthLogin
    {
     
        public string HocaAdı { get; set; }

        public string Şifresi { get; set; }
      
        public bool BeniHatırla { get; set; } 
        public string message { get; set; }
       
    }
    public class CreateUser
    {
        //public int HocaId { get; set; }
        public string HocaAdı { get; set; }
        public string HocaSoyadı { get; set; }
        public string Ünvanı { get; set; }
        public string Email { get; set; }
        public string Şifresi { get; set; }
        public string ŞifreTekrar { get; set; }
        public string message { get; set; }

      
    }
    public class SifremiUnuttum
    {
        [Required(ErrorMessage = "Email boş bırakılamaz")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email adresi geçersiz")]
        public string Şifresi { get; set; }
        public string Email { get; set; }
        public string message { get; set; }
      

    }

   
}