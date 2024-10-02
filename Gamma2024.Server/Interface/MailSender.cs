namespace Gamma2024.Server.Interface
{
    public interface MailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
