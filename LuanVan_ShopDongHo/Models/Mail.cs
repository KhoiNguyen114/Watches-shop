using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace LuanVan_ShopDongHo.Models
{
    public class Mail
    {

        public async Task<bool> sendMail(string customerMailAddress,string subject, string mailContent)
        {
            var fromEmail = ConfigurationManager.AppSettings["FromEmailAddress"];
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"];
            var fromEmailPassword = ConfigurationManager.AppSettings["MailAuthPass"];
            var stmpHost = ConfigurationManager.AppSettings["MailServer"];
            var stmpPort = ConfigurationManager.AppSettings["Port"];
            bool EnableSSL = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

            string bodyMail = mailContent;

            MailMessage message = new MailMessage(
                from: new MailAddress(fromEmail, fromEmailDisplayName, Encoding.UTF8),
                to: new MailAddress(customerMailAddress)
                );

            message.Subject = subject;
            message.Body = mailContent;
            message.BodyEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            int port;
            bool stmpPortAvail = int.TryParse(stmpPort, out port);
            if(stmpPortAvail == true)
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Port = port;
                    client.Host = stmpHost;
                    client.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
                    client.EnableSsl = EnableSSL;
                    try
                    {
                        await client.SendMailAsync(message);
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                        return false;
                    }
                }
            }
            return false; 

        }
    }
}