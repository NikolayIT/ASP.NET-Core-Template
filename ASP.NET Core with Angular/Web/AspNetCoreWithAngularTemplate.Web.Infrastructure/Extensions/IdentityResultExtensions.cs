namespace AspNetCoreWithAngularTemplate.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;

    public static class IdentityResultExtensions
    {
        public static string GetFirstError(this IdentityResult identityResult)
        {
            if (identityResult == null)
            {
                throw new ArgumentNullException(nameof(identityResult));
            }

            return identityResult.Errors.Select(e => e.Description).FirstOrDefault();
        }
    }
}
