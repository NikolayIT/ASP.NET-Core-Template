namespace AspNetCoreTemplate.Data.Seeding
{
    using System;
    using System.Linq;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class ApplicationDbContextSeeder
    {
        public void Seed(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            this.Seed(dbContext, roleManager);
        }

        public void Seed(ApplicationDbContext dbContext, RoleManager<ApplicationRole> roleManager)
        {
            this.SeedRoles(roleManager);
        }

        private void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            this.SeedRole(GlobalConstants.AdministratorRoleName, roleManager);
        }

        private void SeedRole(string roleName, RoleManager<ApplicationRole> roleManager)
        {
            var role = roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult();
            if (role == null)
            {
                var result = roleManager.CreateAsync(new ApplicationRole(roleName)).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
