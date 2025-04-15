namespace AspNetCoreTemplate.Web.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    using AspNetCoreTemplate.Data;
    using AspNetCoreTemplate.Data.Common;
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Repositories;
    using AspNetCoreTemplate.Services.Data;
    using AspNetCoreTemplate.Services.Messaging;
    using AspNetCoreTemplate.Services.ServiceLifetimes;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddCookiePolicy(this IServiceCollection services)
            => services
                .Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

        public static IServiceCollection AddRazor(this IServiceCollection services)
        {
            services.AddRazorPages();
            return services;
        }

        public static IServiceCollection AddDatabaseExceptionPage(
            this IServiceCollection services,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                return services.AddDatabaseDeveloperPageExceptionFilter();
            }

            return services;
        }

        public static IServiceCollection AddControllersAndViews(this IServiceCollection services)
        {
            services
                .AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .AddRazorRuntimeCompilation();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>))
                .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
                .AddScoped<IDbQueryRunner, DbQueryRunner>();

        public static IServiceCollection AddEmailSender(this IServiceCollection services)
            => services.AddTransient<IEmailSender, NullMessageSender>();

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var singletonInterfaceType = typeof(ISingletonService);
            var scopedInterfaceType = typeof(IScopedService);
            var transientInterfaceType = typeof(ITransientService);

            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                .SelectMany(a => a.GetReferencedAssemblies())
                .Where(a => a.Name != null && a.Name.Contains("service", StringComparison.OrdinalIgnoreCase))
                .Distinct()
                .ToList()
                .ForEach(assemblyName => Assembly.Load(assemblyName));

            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName != null && a.FullName.Contains(".service", StringComparison.OrdinalIgnoreCase))
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => t.IsClass && !t.IsAbstract);

            foreach (var implementationType in types)
            {
                var serviceInterfaces = implementationType
                    .GetInterfaces()
                    .Where(i =>
                        i != singletonInterfaceType &&
                        i != scopedInterfaceType &&
                        i != transientInterfaceType &&
                        i.IsPublic);

                foreach (var serviceType in serviceInterfaces)
                {
                    if (singletonInterfaceType.IsAssignableFrom(serviceType))
                    {
                        services.AddSingleton(serviceType, implementationType);
                    }
                    else if (scopedInterfaceType.IsAssignableFrom(serviceType))
                    {
                        services.AddScoped(serviceType, implementationType);
                    }
                    else if (transientInterfaceType.IsAssignableFrom(serviceType))
                    {
                        services.AddTransient(serviceType, implementationType);
                    }
                }
            }

            return services;
        }
    }
}
