[assembly: Microsoft.AspNetCore.Hosting.HostingStartup(typeof(AspNetCoreTemplate.Web.Areas.Identity.IdentityHostingStartup))]

namespace AspNetCoreTemplate.Web.Areas.Identity
{
    using Microsoft.AspNetCore.Hosting;

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
