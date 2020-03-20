namespace AspNetCoreTemplate.Web.Tests
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;

    public sealed class SeleniumServerFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private readonly Process process;

        private IWebHost host;

        public SeleniumServerFactory()
        {
            this.ClientOptions.BaseAddress = new Uri("https://localhost"); // will follow redirects by default
            this.CreateServer(this.CreateWebHostBuilder());

            this.process = new Process
                       {
                           StartInfo = new ProcessStartInfo
                                       {
                                           FileName = "selenium-standalone",
                                           Arguments = "start",
                                           UseShellExecute = true,
                                       },
                       };
            this.process.Start();
        }

        public string RootUri { get; set; }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            this.host = builder.Build();
            this.host.Start();
            this.RootUri = this.host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault(); // Last is https://localhost:5001!

            // Fake Server we won't use...this is lame. Should be cleaner, or a utility class
            return new TestServer(new WebHostBuilder().UseStartup<FakeStartup>());
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = WebHost.CreateDefaultBuilder(Array.Empty<string>());
            builder.UseStartup<TStartup>();
            return builder;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.host.Dispose();
                this.process.CloseMainWindow(); // Be sure to stop Selenium Standalone
            }
        }

        public class FakeStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
            }

            public void Configure()
            {
            }
        }
    }
}
