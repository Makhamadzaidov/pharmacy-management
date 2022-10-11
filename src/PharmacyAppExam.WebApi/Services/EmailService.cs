using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using PharmacyAppExam.WebApi.Interfaces.Services;

namespace PharmacyAppExam.WebApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfigurationSection _config;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration.GetSection("Email");
        }
        public async Task SendAsync(string email, string message)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_config["EmailName"]));
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = "Verification Code";
            mail.Body = new TextPart(TextFormat.Html) { Text = message };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Host"], 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailName"], _config["Password"]);
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);
        }
    }
}
