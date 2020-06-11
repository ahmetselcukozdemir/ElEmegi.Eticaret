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
    public class ProductComments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ get; set; }
        [DisplayName("Adınız ve soyadınız")]
        public string Name{ get; set; }
        [DisplayName("Yorum Tarihi")]
        public DateTime CreatedDate{ get; set; }
        [DisplayName("Yorum")]
        public string Content{ get; set; }
        public int ProductID { get; set; }
        public virtual Product Product{ get; set; }
        public bool IsApproved { get; set; }
    }
}
