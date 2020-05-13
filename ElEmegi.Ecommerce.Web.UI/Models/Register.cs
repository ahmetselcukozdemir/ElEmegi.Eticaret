using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Adınız")]
        public string Name{ get; set; }
        [Required]
        [DisplayName("Soyadınız")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("E-posta")]
        [EmailAddress(ErrorMessage = "E-posta adresinizi doğru girin.")]
        public string Email{ get; set; }
        [Required]
        [DisplayName("Şifre")]
        public string Password{ get; set; }
        [Required]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor.")]
        public string RePassword{ get; set; }
        [Required]
        [DisplayName("Cinsiyet")]
        public bool Gender{ get; set; }
    }
}