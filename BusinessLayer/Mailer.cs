using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using WCFInterfaces;
using DataLayer.Mapping;

namespace BusinessLayer
{
    class Mailer
    {
        private SmtpClient client;
        private MappingUsers mappingUsers = new MappingUsers();

        public Mailer()
        {
            client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("exiawhitehat@gmail.com", "exiaantibadcompany");
        }

        public void SendResultMail(string file, string user, STG message)
        {
            var userEmail = mappingUsers.GetEmail(user);
            MailMessage mail = new MailMessage("donotreply@exiawhitehat.com", userEmail);
            mail.Subject = "BruteForce results";

            string key = message.data[1].ToString();
            string text = message.data[2].ToString();
            double percent = double.Parse(message.data[3].ToString());
            string emails = message.data[4].ToString();

            mail.Body = 
                "<h1>BruteForce result</h1>" +
                "<p>File : " + file + "</p>" +
                "<p>Key : " + key + "</p>" +
                "<p>Emails found : " + emails + "</p>" +
                "<p>Percentage of French words : " + percent + "%</p>" +
                "<p>Decrypted file : " + text + "</p>"
            ;
            mail.IsBodyHtml = true;

            client.Send(mail);
        }
    }
}
