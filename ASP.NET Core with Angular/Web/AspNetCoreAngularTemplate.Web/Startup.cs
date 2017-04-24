namespace AspNetCoreAngularTemplate.Web
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;

    using AspNetCoreAngularTemplate.Data;
    using AspNetCoreAngularTemplate.Data.Common.Repositories;
    using AspNetCoreAngularTemplate.Data.Models;
    using AspNetCoreAngularTemplate.Data.Repositories;
    using AspNetCoreAngularTemplate.Data.Seeding;
    using AspNetCoreAngularTemplate.Services.Messaging;
    using AspNetCoreAngularTemplate.Web.Infrastructure.Mapping;
    using AspNetCoreAngularTemplate.Web.Infrastructure.Middlewares.Auth;
    using AspNetCoreAngularTemplate.Web.Models.AccountViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(
                    options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredLength = 6;
                    })
                .AddUserStore<ApplicationUserStore>()
                .AddRoleStore<ApplicationRoleStore>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Data
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Add application services.
            services.AddTransient<IEmailSender, DoNothingMessageSender>();
            services.AddTransient<ISmsSender, DoNothingMessageSender>();

            // Identity stores
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(LoginViewModel).GetTypeInfo().Assembly,
                typeof(LoginViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var seeder = new ApplicationDbContextSeeder();
                seeder.Seed(db, app.ApplicationServices);
            }

            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // TODO: Extract to appsettings!
            const string Secret = "AspNetCoreAngularTemplateSecretChangeItInProductionWithSomethingDifferent!!!";
            const string Issuer = "AspNetCoreAngularTemplateIssuer";
            const string Audience = "AspNetCoreAngularTemplateAudience";

            var options = new TokenProviderOptions
                          {
                              Path = "/api/account/login",
                              Expiration = TimeSpan.FromDays(15),
                              Issuer = Issuer,
                              Audience = Audience,
                              Secret = Secret,
                              PrincipalResolver = PrincipalResolver
                          };

            app.UseJwtBearerTokens(options);

            app.UseMvc(routes => routes.MapRoute("DefaultApi", "api/{controller}/{action}/{id?}"));
        }

        private static async Task<GenericPrincipal> PrincipalResolver(HttpContext context)
        {
            var email = context.Request.Form["email"];
            var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var roles = await userManager.GetRolesAsync(user);
                var password = context.Request.Form["password"];
                var isValidPassword = await userManager.CheckPasswordAsync(user, password);
                if (isValidPassword)
                {
                    var identity = new GenericIdentity(email, "Token");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    return new GenericPrincipal(identity, roles.ToArray());
                }
            }

            return null;
        }
    }
}
