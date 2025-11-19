using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using WebSite.Models;

namespace WebSite.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string htmlBody)
        {
            var message = new MimeMessage();

            //подготовка письма
            message.From.Add(new MailboxAddress(_settings.FromName, _settings.UserName));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlBody };

            try
            {
                using (var client = new SmtpClient())
                {
                    //отправка
                    await client.ConnectAsync(_settings.Host, _settings.Port, _settings.UseSSL);
                    await client.AuthenticateAsync(_settings.UserName, _settings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }
    }
}
