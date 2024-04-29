using Domain.IServices;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

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
            string smtpServer = _configuration["MailSettings:SmtpServer"];
            int port = int.Parse(_configuration["MailSettings:Port"]);
            string senderEmail = _configuration["MailSettings:SenderEmail"];
            string senderPassword = _configuration["MailSettings:SenderPassword"];


            MailMessage mail = new MailMessage(senderEmail, to);
            SmtpClient client = new SmtpClient(senderPassword, port)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email:");
            }
            finally
            {
                client.Dispose();
                mail.Dispose();
            }
        }
    }
}
