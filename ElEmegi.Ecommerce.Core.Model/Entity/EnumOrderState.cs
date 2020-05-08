using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElEmegi.Ecommerce.Core.Model.Entity
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekleniyor..")]
        Waiting,
        [Display(Name = "Ürünler Hazırlanıyor..")]
        Prepare,
        [Display(Name = "Kargoya verildi.")]
        Cargo,
        [Display(Name = "Sipariş İptal Edildi")]
        Cancel,
        [Display(Name = "Tamamlandı..")]
        Completed
    }
}
