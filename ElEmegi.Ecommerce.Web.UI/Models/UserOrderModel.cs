using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class UserOrderModel
    {
        public int ID { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public EnumOrderState OrderState { get; set; }
        public DateTime OrderDate { get; set; }
    }
}