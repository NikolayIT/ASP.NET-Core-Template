namespace AspNetCoreAngularTemplate.Web.Infrastructure.Middlewares.Auth
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    using Newtonsoft.Json;

    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate next;
        private readonly TokenProviderOptions options;

        public TokenProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options)
        {
            this.next = next;
            this.options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(this.options.Path, StringComparison.Ordinal))
            {
                return this.next(context);
            }

            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return this.GenerateToken(context);
            }

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync("Bad request");
        }

        private async Task GenerateToken(HttpContext context)
        {
            var principal = await this.options.PrincipalResolver(context);
            if (principal == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("Invalid email or password.");
                return;
            }

            var now = DateTime.UtcNow;
            var unixTimeSeconds = (long)Math.Round(
                (now.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

            var existingClaims = principal.Claims.ToDictionary(cl => cl.Type, cl => cl);

            var systemClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, principal.Identity.Name),
                new Claim(JwtRegisteredClaimNames.Jti, await this.options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, unixTimeSeconds.ToString(), ClaimValueTypes.Integer64)
            };

            systemClaims.ForEach(cl => existingClaims[cl.Type] = cl);

            var jwt = new JwtSecurityToken(
                issuer: this.options.Issuer,
                audience: this.options.Audience,
                claims: existingClaims.Values,
                notBefore: now,
                expires: now.Add(this.options.Expiration),
                signingCredentials: this.options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)this.options.Expiration.TotalSeconds,
                roles = existingClaims.Values.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
            };

            var serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
        }
    }
}
