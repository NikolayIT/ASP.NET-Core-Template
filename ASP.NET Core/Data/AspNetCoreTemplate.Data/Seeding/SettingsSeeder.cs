namespace AspNetCoreTemplate.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models;

    internal class SettingsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Settings.Any())
            {
                return;
            }

            await dbContext.Settings.AddAsync(new Setting { Name = "Setting1", Value = "value1" });
        }
    }
}
