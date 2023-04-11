using E_CommerceWebApplication.BLL.Service;
using Serilog;
//using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebApplication.BLL.ServiceRepository
{
    public class ServiceRepo : IEmail
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("amolecom2017@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod=SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("amolecom2017@gmail.com", "oeggrsnoozshqhgj");

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
               Log.Error(ex.Message.ToString());
            }
            return false;
        }

        public bool SendForgetPasswordEmail(string userEmail, string token)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("amolecom2017@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Rest your password";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = token;

            SmtpClient client = new SmtpClient();



            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("amolecom2017@gmail.com", "oeggrsnoozshqhgj");
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                //Log.Error(ex.Message.ToString());
            }
            return false;
        }
    }
}
