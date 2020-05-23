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
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ get; set; }
        [DisplayName("Üye Adı")]
        public string Name{ get; set; }
        [DisplayName("Üye Soyadı")]
        public string Surname{ get; set; }
        [DisplayName("Üye Telefon")]
        public string Phone{ get; set; }
        [DisplayName("Üye Email")]
        public string Email{ get; set; }
        [DisplayName("Üye Şifre")]
        public string Password{ get; set; }
        [DisplayName("Üye Fotograf")]
        public string Photo{ get; set; }
        public bool IsAdmin{ get; set; }
        public bool IsActive{ get; set; }

        public List<Product> Products { get; set; }
    }
}
