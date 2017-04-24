namespace AspNetCoreTemplate.Services.Messaging
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Messaging.SendGrid;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    public class SendGridEmailSender : IEmailSender
    {
        private const string AuthenticationScheme = "Bearer";

        private const string BaseUrl = "https://api.sendgrid.com/v3/";

        private const string SendEmailUrlPath = "mail/send";

        private readonly string fromAddress;

        private readonly string fromName;

        private readonly HttpClient httpClient;

        private readonly ILogger logger;

        public SendGridEmailSender(ILoggerFactory loggerFactory, string apiKey, string fromAddress, string fromName)
        {
            this.fromAddress = fromAddress;
            this.fromName = fromName;
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentOutOfRangeException(nameof(apiKey));
            }

            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(AuthenticationScheme, apiKey);
            this.httpClient.BaseAddress = new Uri(BaseUrl);
            this.logger = loggerFactory.CreateLogger<SendGridEmailSender>();
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(this.fromAddress))
            {
                throw new ArgumentOutOfRangeException(nameof(this.fromAddress));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentOutOfRangeException(nameof(email));
            }

            if (string.IsNullOrWhiteSpace(subject) && string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Subject and/or message must be provided.");
            }

            var msg = new SendGridMessage(
                new SendGridEmail(email),
                subject,
                new SendGridEmail(this.fromAddress, this.fromName),
                message);
            try
            {
                var json = JsonConvert.SerializeObject(msg);
                var response = await this.httpClient.PostAsync(
                                   SendEmailUrlPath,
                                   new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    // See if we can read the response for more information, then log the error
                    var errorJson = await response.Content.ReadAsStringAsync();
                    throw new Exception(
                        $"SendGrid indicated failure! Code: {response.StatusCode}, reason: {errorJson}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Exception during sending email: {ex}");
            }
        }
    }
}
