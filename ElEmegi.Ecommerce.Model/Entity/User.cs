using System;
using System.Collections.Generic;
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

        public string Name{ get; set; }
        public string Surname{ get; set; }
        public string Email{ get; set; }
        public string Password{ get; set; }
        public bool IsActive{ get; set; }
        public string Photo{ get; set; }
        public DateTime LastActivityDate{ get; set; }
        public bool Gender{ get; set; }
     

    }
}
