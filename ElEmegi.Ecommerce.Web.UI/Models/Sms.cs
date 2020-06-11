using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class Sms
    {
        public void PhoneControl(string phone)
        {
            //onaylı telefon mu ?
            //WaitingForDeadline
        }

        public void DiscountMessage(string phone)
        {
            //indirimler başladı sms'i
            //WaitingForDeadline
        }

        public void CancelOrder(string phone)
        {
            //siparişin iptal edildi.
            //WaitingForDeadline
        }
    }
}