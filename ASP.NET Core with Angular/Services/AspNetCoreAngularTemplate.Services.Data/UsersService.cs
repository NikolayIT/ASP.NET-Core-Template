namespace AspNetCoreAngularTemplate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreAngularTemplate.Data.Common.Repositories;
    using AspNetCoreAngularTemplate.Data.Models;

    using Microsoft.AspNetCore.Identity;

    // TODO: Extract to AspNetCoreAngularTemplate.Services.Identity
    public class UsersService : IUsersService
    {
        private readonly IRepository<ApplicationUser> users;

        private readonly IUserStore<ApplicationUser> userStore;

        private readonly IRoleStore<ApplicationRole> roleStore;

        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            IRepository<ApplicationUser> users,
            IUserStore<ApplicationUser> userStore,
            IRoleStore<ApplicationRole> roleStore,
            UserManager<ApplicationUser> userManager)
        {
            this.users = users;
            this.userStore = userStore;
            this.roleStore = roleStore;
            this.userManager = userManager;
        }

        public IEnumerable<string> GetUserRoles(string id)
        {
            var roles = this.userManager.GetRolesAsync(new ApplicationUser { Id = id }).Result;
            return roles;
        }

        public async Task<IdentityResult> CreateAsync(string userName, string password)
        {
            var user = new ApplicationUser { Email = userName, UserName = userName };

            var result = await this.userManager.CreateAsync(user, password);

            return result;
        }
    }
}
