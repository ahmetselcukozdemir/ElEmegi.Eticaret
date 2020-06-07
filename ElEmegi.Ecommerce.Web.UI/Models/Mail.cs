using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class Mail
    {
        public void OrderMail(string email)
        {
            //siparişin alındı,teşekkürler
            var fromAddress = new MailAddress("ahmetselcukozdemir01@gmail.com");
            var toAddress = new MailAddress(email);
            const string subject = "Siparişiniz Alındı ! :)";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromAddress.Address, "")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = "" })
                {
                    smtp.Send(message);
                }
            }
        }

        public void NewUserMail(string email)
        {
            //aramıza hoşgeldin yeni kullanıcı 
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

    }
}