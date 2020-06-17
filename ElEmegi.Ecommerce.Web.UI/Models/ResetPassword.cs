using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class ResetPassword
    {
        [DisplayName("Yeni Şifreniz")]
        [Required]
        public string Password { get; set; }
        [DisplayName("Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor.")]
        public string RePassword { get; set; }
        [DisplayName("Sıfırlama Kodunuz")]
        [Required]
        public string ResetCode { get; set; }
    }
}