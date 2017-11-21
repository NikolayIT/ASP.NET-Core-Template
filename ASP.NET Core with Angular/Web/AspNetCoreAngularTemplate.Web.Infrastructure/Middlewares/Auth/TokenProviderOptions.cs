namespace AspNetCoreAngularTemplate.Web.Infrastructure.Middlewares.Auth
{
    using System;
    using System.Security.Principal;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.IdentityModel.Tokens;

    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(15);

        public Func<HttpContext, Task<GenericPrincipal>> PrincipalResolver { get; set; }

        public Func<Task<string>> NonceGenerator { get; set; } = () => Task.FromResult(Guid.NewGuid().ToString());

        internal SigningCredentials SigningCredentials { get; set; }
    }
}
