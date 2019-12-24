namespace AspNetCoreTemplate.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null);
    }
}
