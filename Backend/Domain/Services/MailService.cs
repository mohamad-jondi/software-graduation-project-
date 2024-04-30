using Domain.IServices;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Domain.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendMail(string to, string subject, string body)
        {
            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", _configuration["MailSettings:SenderEmail"])); 
            message.To.Add(new MailboxAddress("", to)); 
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(_configuration["MailSettings:SenderEmail"], _configuration["MailSettings:SenderPassword"]);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch { return false;  }
            return true;
        }
    }
    }
