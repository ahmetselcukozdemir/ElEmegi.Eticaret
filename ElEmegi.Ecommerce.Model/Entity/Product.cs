using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElEmegi.Ecommerce.Model.Entity
{
    public class Product
    {
        public int ID { get; set; }
        public string EncryptedString{ get; set; }
        [DisplayName("Ürün Adı")]
        public string Name { get; set; }
        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Ürün Açıklaması 2")]
        public string DescriptionTwo { get; set; }
        [DisplayName("Ürün Fiyat")]
        public double Price { get; set; }
        [DisplayName("Ürün Fotograf")]
        public string Image { get; set; }
        [DisplayName("Ürün Fotograf 2")]
        public string ImageTwo{ get; set; }
        [DisplayName("Ürün Fotograf 3")]
        public string ImageThree{ get; set; }
        [DisplayName("Ürün Stok")]
        public int Stock { get; set; }
        [DisplayName("Aktif Mi ?")]
        public bool IsApproved { get; set; }
        [DisplayName("Yayında Mı ?")]
        public bool IsHome { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreatedDate{ get; set; }
        [DisplayName("Kategori")]
        public int CategoryId { get; set; }
        [DisplayName("Kategori")]
        public Category Category { get; set; }
        public int MemberID{ get; set; }
        public virtual Member Member{ get; set; }
    }
}
