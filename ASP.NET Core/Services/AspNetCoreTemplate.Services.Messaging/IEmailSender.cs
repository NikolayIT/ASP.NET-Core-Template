namespace AspNetCoreTemplate.Services.Messaging
{
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
