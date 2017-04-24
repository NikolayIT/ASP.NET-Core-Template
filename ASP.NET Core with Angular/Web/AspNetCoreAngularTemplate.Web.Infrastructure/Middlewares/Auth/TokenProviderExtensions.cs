namespace AspNetCoreAngularTemplate.Web.Infrastructure.Middlewares.Auth
{
    using System;
    using System.Text;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public static class TokenProviderExtensions
    {
        public static void UseJwtBearerTokens(this IApplicationBuilder app, TokenProviderOptions options)
        {
            ValidateArgs(app, options);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Secret));
            options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,
                    ValidateAudience = true,
                    ValidAudience = options.Audience,
                    ValidateLifetime = true
                }
            });
        }

        private static void ValidateArgs(IApplicationBuilder app, TokenProviderOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrEmpty(options.Secret))
            {
                throw new ArgumentException(nameof(options.Secret));
            }

            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentException(nameof(options.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentException(nameof(options.Audience));
            }

            if (options.PrincipalResolver == null)
            {
                throw new ArgumentNullException(nameof(options.PrincipalResolver));
            }
        }
    }
}
