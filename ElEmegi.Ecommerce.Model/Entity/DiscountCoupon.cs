using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace ElEmegi.Ecommerce.Model.Entity
{
   public class DiscountCoupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ get; set; }
        [DisplayName("Kupon Adı")]
        public string Name{ get; set; }
        [DisplayName("Kupon Açıklama")]
        public string Description { get; set; }
        [DisplayName("Kupon İndirim Yüzdesi")]
        public int Percent { get; set; }
        public DateTime CreatedDate{ get; set; }
        [DisplayName("İndirim Bitiş Tarihi")]
        public DateTime ExpirationDate { get; set; }
        public bool IsActive{ get; set; }

    }
}
