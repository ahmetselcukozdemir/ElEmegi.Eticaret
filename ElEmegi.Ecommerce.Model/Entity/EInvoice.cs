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
    public class EInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("E-Fatura Numarası")]
        public string InvoiceNumber { get; set; }
        [DisplayName("E-Fatura Tarihi")]
        public DateTime CreatedTime{ get; set; }
        [DisplayName("Adet")]
        public int Quantity { get; set; }
        [DisplayName("Birim Fiyatı")]
        public double UnitPrice { get; set; }
        [DisplayName("Fiyat")]
        public double Price { get; set; }
        public int OrderLineID { get; set; }
        public OrderLine OrderLine { get; set; }
    }
}
