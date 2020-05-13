﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElEmegi.Ecommerce.Core.Model.Entity
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{ get; set; }
        public string Name{ get; set; }
        public string Surname{ get; set; }
        public string Phone{ get; set; }
        public string Email{ get; set; }
        public string Password{ get; set; }
        public string Photo{ get; set; }
        public bool IsAdmin{ get; set; }
        public bool IsActive{ get; set; }

        public List<Product> Products { get; set; }
    }
}
