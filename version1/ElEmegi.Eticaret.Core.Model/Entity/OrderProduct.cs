using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ElEmegi.Eticaret.Core.Model.Entity
{
    public class OrderProduct : EntityBase
    {
        public int OrderID{ get; set; }
        public int ProductID{ get; set; }
        public Product Product{ get; set; }
        public int Quantity{ get; set; }
    }
}
