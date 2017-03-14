using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace AuthApp.Controllers
{
    public static class PasswordRecoveryManager
    {
        public static void SendByEmail(string email, string username, string password)
        {
            string sender = ConfigurationManager.AppSettings["smtp_UserName"];
            string pass = ConfigurationManager.AppSettings["smtp_Password"];
            MailMessage message = new MailMessage(sender, email);
            message.Subject = string.Format("Восстановление пароля");
            message.Body = string.Format("<h2>Восстановление пароля \"{0}\"</h2><br/><p>Ваш пароль: {1}</p>", username, password);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = sender,
                    Password = pass
                };
                smtp.Credentials = credentials;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                
                try
                {
                    smtp.Send(message);
                }
                catch (SmtpException e)
                {

                }
            }
        }

        public static void SendByEmailGenerated(string email)
        {

        }
    }
}