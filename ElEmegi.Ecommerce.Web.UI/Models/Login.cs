using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class Login
    {
        [Required]
        [DisplayName("E-mail Adresi")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [DisplayName("Beni hatırla")]
        public bool RememberMe { get; set; }
        public string Captcha { get; set; }
    }
}