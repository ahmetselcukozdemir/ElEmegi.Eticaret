using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElEmegi.Ecommerce.Model.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string Name{ get; set; }
        [DisplayName("Kullanıcı Soyadı")]
        public string Surname{ get; set; }
        [DisplayName("Kullanıcı Email")]
        public string Email{ get; set; }
        [DisplayName("Kullanıcı Şifre")]
        public string Password{ get; set; }
        public bool IsActive{ get; set; }
        [DisplayName("Kullanıcı Fotograf")]
        public string Photo{ get; set; }
        public DateTime LastActivityDate{ get; set; }
        public List<UserAddress> UserAddresses{ get; set; }
    }
}
