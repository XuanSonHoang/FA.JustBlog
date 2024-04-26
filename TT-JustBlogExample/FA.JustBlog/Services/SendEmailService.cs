﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;


namespace FA.JustBlog.Services
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public interface ISendEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
    public class SendEmailService : IEmailSender
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<SendEmailService> _logger;
        public SendEmailService(IOptions<MailSettings> mailSettings, ILogger<SendEmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
            message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();

            //use smtp client to send email of MailKit
            using var  smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect (_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(message);
            } catch(Exception ex)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailsavefile);

                _logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                _logger.LogError(ex.Message);

                _logger.LogError(ex, "An error occurred while sending email");
            }
            finally
            {
                smtp.Disconnect(true);
                smtp.Dispose();
            }
            _logger.LogInformation("Send Email To " + email);
        }
    }
}
