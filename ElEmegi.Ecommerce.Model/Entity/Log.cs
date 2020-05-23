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
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [DisplayName("Operasyon Tipi")]
        public string OperationType { get; set; }
        [DisplayName("Log Tarihi")]
        public DateTime CreateDate{ get; set; }
        public int MemberID{ get; set; }
    }
}
