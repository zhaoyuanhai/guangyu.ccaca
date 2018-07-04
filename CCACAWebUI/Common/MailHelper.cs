using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Text;

namespace CCACAWebUI.Common
{
    public class MailHelper
    {
        public static string UserName { get; set; }

        public static string Passwrod { get; set; }

        public static string ServerAddress { get; set; }

        public static string DisplayName { get; set; }

        public static void SendMail(string toAddress, string title, string content)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(UserName, DisplayName);
            mailMessage.To.Add(toAddress);
            mailMessage.Body = content;
            mailMessage.Subject = title;


            SmtpClient client = new SmtpClient(ServerAddress);

            client.EnableSsl = false;
            client.UseDefaultCredentials = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(UserName, Passwrod);
            client.Send(mailMessage);
        }

        public static string RandomNumber()
        {
            Random r = new Random();
            string str = "";
            for (int i = 0; i < 4; i++)
            {
                str += r.Next(10);
            }
            return str;
        }
    }
}
