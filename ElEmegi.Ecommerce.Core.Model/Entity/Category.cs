using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElEmegi.Ecommerce.Core.Model.Entity
{
    public class Category
    {
        public int ID { get; set; }
        [DisplayName("Kategori Adı")]
        [StringLength(maximumLength: 15, ErrorMessage = "En fazla 15 karakter girebilirsiniz.")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
