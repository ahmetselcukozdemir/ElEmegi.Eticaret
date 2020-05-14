using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElEmegi.Ecommerce.Core.Model.Entity
{
    public class Order
    {
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }

        public string Name { get; set; }
        public string Surname{ get; set; }
        public string AddressTitle { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string Road{ get; set; }
        public string Type { get; set; }
        public string PostCode { get; set; }
        public bool OrderUserControl{ get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public int UserID { get; set; }
    }
    public class OrderLine
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
     
    
    }
}
