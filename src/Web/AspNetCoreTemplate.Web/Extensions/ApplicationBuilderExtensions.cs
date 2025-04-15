namespace AspNetCoreTemplate.Web.Extensions
{
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data;
    using AspNetCoreTemplate.Data.Seeding;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> UsePrepareDbAsync(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
            await new ApplicationDbContextSeeder().SeedAsync(dbContext, services.ServiceProvider);

            return app;
        }

        public static IApplicationBuilder UseAutoMapper(this IApplicationBuilder app)
        {
            AutoMapperConfig
                .RegisterMappings(typeof(ErrorViewModel)
                .GetTypeInfo().Assembly);

            return app;
        }

        public static IApplicationBuilder UseDeveloperExceptionPage(
            this IApplicationBuilder app,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }

        public static IApplicationBuilder UseMigrationsEndPoint(
            this IApplicationBuilder app,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }

            return app;
        }

        public static IApplicationBuilder UseExceptionHandler(
           this IApplicationBuilder app,
           IWebHostEnvironment environment)
        {
            if (!environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            return app;
        }

        public static IApplicationBuilder UseHsts(
           this IApplicationBuilder app,
           IWebHostEnvironment environment)
        {
            if (!environment.IsDevelopment())
            {
                app.UseHsts();
            }

            return app;
        }

        public static IEndpointRouteBuilder UseMapRoutes(this IEndpointRouteBuilder app)
        {
            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            return app;
        }
    }
}
