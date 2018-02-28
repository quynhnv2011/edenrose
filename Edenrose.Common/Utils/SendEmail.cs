using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;


namespace Vks.Common.Utils
{
    public class SendEmail
    {
        public static bool Send(string toEmail, string subject, string body, List<string> lstCC = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(toEmail);
                mail.From = new MailAddress("noreply.quynhnv@gmail.com");
                mail.Body = body;
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("noreply.quynhnv@gmail.com", "quynhvan"); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                return false;
            }

        }
    }
}
