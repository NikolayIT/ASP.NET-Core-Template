namespace AspNetCoreWithAngularTemplate.Web.Infrastructure.Extensions
{
    using System;
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentNullException(nameof(claimsPrincipal));
            }

            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                return claimsPrincipal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }

            return null;
        }
    }
}
