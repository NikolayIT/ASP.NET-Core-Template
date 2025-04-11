namespace AspNetCoreTemplate.Web
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Web.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder
                .Services
                .AddDatabase(builder.Configuration)
                .AddIdentity()
                .AddCookiePolicy()
                .AddRazor()
                .AddDatabaseExceptionPage(builder.Environment)
                .AddControllersAndViews()
                .AddRepositories()
                .AddEmailSender()
                .AddServices();

            var app = builder.Build();
            await app.UsePrepareDbAsync();
            app
                .UseAutoMapper()
                .UseDeveloperExceptionPage(app.Environment)
                .UseMigrationsEndPoint(app.Environment)
                .UseExceptionHandler(app.Environment)
                .UseHsts(app.Environment)
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseCookiePolicy()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization();

            app.UseMapRoutes();
            await app.RunAsync();
        }
    }
}
