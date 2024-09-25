using Gamma2024.Server.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Gamma2024.Server.Models.Email
{

    public class EmailService : IEmailSender
    {
        private readonly EmailConfiguration _options;
        public EmailService(IOptions<EmailConfiguration> options)
        {
            _options = options.Value;
        }
        public void Send(string to, string subject, string html, string? from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _options.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };


            // sendemail
            using var smtp = new SmtpClient();

            smtp.Connect(_options.SmtpServer, _options.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.UserName, _options.Password);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
