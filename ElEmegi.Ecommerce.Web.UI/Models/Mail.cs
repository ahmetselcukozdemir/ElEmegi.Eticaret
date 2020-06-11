using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using ElEmegi.Ecommerce.Model.Entity;
using System.Web.Mvc;


namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class Mail
    {
        private DataContext db = new DataContext();
        public void OrderMail(string email,string order_number,string total_price)
        {
            //siparişin alındı,teşekkürler
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPuser"], ConfigurationManager.AppSettings["SMTPpassword"]);
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                message.To.Add(email);
                message.From = new MailAddress(ConfigurationManager.AppSettings["SMTPuser"]);
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = "Siparişiniz Bize Ulaştı :) ";
                message.Body = CreateBodyMailOrder(order_number,total_price);
                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
        private string CreateBodyMailOrder(string order_number, string total_price)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Views/MailTemplate/OrderMail.cshtml")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{total}", total_price);
            body = body.Replace("{order_number}", order_number);
            return body;
        }
    
        public void NewUserMail(string email,string name, string surname)
        {
            //aramıza hoşgeldin yeni kullanıcı 
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPuser"], ConfigurationManager.AppSettings["SMTPpassword"]);
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            message.To.Add(email);
            message.From = new MailAddress(ConfigurationManager.AppSettings["SMTPuser"]);
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "Aramıza hoşgeldin :) ";
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Views/MailTemplate/NewUserMail.cshtml")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{name}", name);
            body = body.Replace("{surname}", surname);
            message.Body = body;
            smtpClient.Send(message);
        }

        public void DiscountNews(IEnumerable<User> users)
        {
            // patron çıldırdı,indirim günleri başladı :) 
        }

        public void HappyBirthdayUser(List<User> users)
        {
            //bugun doğan kullanıcılar için bir tebrik maili. 
        }
        public void HappyBirthdayMember(List<Member> members)
        {
            //bugun doğan üyeler için bir tebrik maili. 
        }

        public void ForgotMyPassword(string email)
        {
            //şifremi unuttum
        }

        public void CancelOrder(string email)
        {
            //siparişin iptal edildi e-maili.
        }
        public Cart GetCart()
        {
            Cart cart = (Cart)HttpContext.Current.Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Current.Session["Cart"] = cart;
            }
            return cart;
        }
    }
}