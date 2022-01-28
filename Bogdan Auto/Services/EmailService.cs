using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Bogdan_Auto.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("bogdan.auto.group@gmail.com"));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using var smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("bogdan.auto.group@gmail.com", "W3lcome1!")//hfckspjinpeqfcfo
                };
                smtp.Credentials = new NetworkCredential("bogdan.auto.group@gmail.com", "W3lcome1!");
                //   var credentials = new NetworkCredential("bogdan.auto.group@gmail.com", "W3lcome1!");
                //   smtp.Connect("smtp.gmail.com", 587, false);
                //   smtp.Authenticate("bogdan.auto.group@gmail.com", "W3lcome1!");
                //   smtp.Send(email);
                //   smtp.Disconnect(true);
                using (var message = new MailMessage("bogdan.auto.group@gmail.com", to)
                {
                    Subject = subject,
                    Body = body                 
                })
                {
                    smtp.Send(message);
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
