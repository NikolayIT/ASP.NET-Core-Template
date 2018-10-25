namespace AspNetCoreWithAngularTemplate.Services.Messaging.SendGrid
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class SendGridMessage
    {
        public const string TypeText = "text";
        public const string TypeHtml = "text/html";

        public SendGridMessage()
        {
        }

        public SendGridMessage(
            SendGridEmail to,
            string subject,
            SendGridEmail from,
            string message,
            IEnumerable<SendGridEmail> bcc = null,
            string type = TypeHtml)
        {
            this.Personalizations = new List<SendGridPersonalization>
            {
                new SendGridPersonalization
                {
                    To = new List<SendGridEmail> { to },
                    Bcc = bcc,
                    Subject = subject,
                },
            };
            this.From = from;
            this.Content = new List<SendGridContent> { new SendGridContent(type, message) };
        }

        [JsonProperty("personalizations")]
        public List<SendGridPersonalization> Personalizations { get; set; }

        [JsonProperty("from")]
        public SendGridEmail From { get; set; }

        [JsonProperty("content")]
        public List<SendGridContent> Content { get; set; }
    }
}
