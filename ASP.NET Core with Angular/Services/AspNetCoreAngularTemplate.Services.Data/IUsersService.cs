namespace AspNetCoreAngularTemplate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public interface IUsersService
    {
        IEnumerable<string> GetUserRoles(string id);

        Task<IdentityResult> CreateAsync(string userName, string password);
    }
}
