namespace Domain.IServices
{
    public interface IMailService
    {
        bool SendMail(string to, string subject, string body);
    }
}
