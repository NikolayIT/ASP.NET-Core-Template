namespace MvcTemplate.Services.Web
{
    using System;
    using System.Text;

    public class IdentifierProvider : IIdentifierProvider
    {
        private const string Salt = ".12312313123";

        public int DecodeId(string urlId)
        {
            var base64EncodedBytes = Convert.FromBase64String(urlId);
            var bytesAsString = Encoding.UTF8.GetString(base64EncodedBytes);
            bytesAsString = bytesAsString.Replace(Salt, string.Empty);
            return int.Parse(bytesAsString);
        }

        public string EncodeId(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
