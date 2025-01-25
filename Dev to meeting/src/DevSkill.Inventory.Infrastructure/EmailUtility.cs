using DevSkill.Inventory.Domain;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DevSkill.Inventory.Infrastructure
{
    public class EmailUtility : IEmailUtility
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailUtility(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string receiverEmail, string receiverName, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
            message.To.Add(new MailboxAddress(receiverName, receiverEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port,
                    _smtpSettings.SmtpEncryption != SmtpEncryptionTypes.Normal);

                client.Timeout = 6000;

                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }

    }
}
