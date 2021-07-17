
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MVCPROJE2_BLOGPROJE.SERVICES.EmailService.Concrete
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Task.Run(() =>
            {
                string fromMail = "mvcblogproje1@gmail.com";
                string fromPassword = "mv,c@blog.proje1";
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = subject;
                message.To.Add(new MailAddress(email));
                message.Body = "<html><body> " + htmlMessage + " </body></html>";
                message.IsBodyHtml = true;
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };
                smtpClient.Send(message);
            });
        }
    }
}
