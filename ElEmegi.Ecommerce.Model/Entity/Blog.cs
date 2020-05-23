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
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ get; set; }
        [DisplayName("Fotograf")]
        public string Image { get; set; }
        [DisplayName("Başlık")]
        public string Title{ get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [DisplayName("İçerik")]
        public string Content { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreateDate{ get; set; }
    }
}
