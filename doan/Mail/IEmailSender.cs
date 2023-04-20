namespace doan.Mail
{
    public interface IEmailSender
    {

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
