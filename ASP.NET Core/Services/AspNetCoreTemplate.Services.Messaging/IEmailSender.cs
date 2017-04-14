namespace AspNetCoreTemplate.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    // Documention: https://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/index.html
}
