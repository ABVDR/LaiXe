using ET.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
namespace sendMail.Service
{
    public class GmailSender : IGmailSender
    {
        private readonly IConfiguration _configuration;
        public GmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSection = _configuration.GetSection("Smtp");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", smtpSection["Username"]));
            message.To.Add(new MailboxAddress("Chao ban ", to));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };
            using (var client = new SmtpClient())
            {
                client.Connect(smtpSection["Server"], int.Parse(smtpSection["Port"]), SecureSocketOptions.StartTls);
                client.Authenticate(smtpSection["Username"], smtpSection["Password"]);//ten tk mk
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
