namespace AspNetCoreAngularTemplate.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreAngularTemplate.Data.Common.Repositories;
    using AspNetCoreAngularTemplate.Data.Models;
    using AspNetCoreAngularTemplate.Web.Infrastructure.Mapping;
    using AspNetCoreAngularTemplate.Web.Models.Settings;

    using Microsoft.AspNetCore.Mvc;

    public class SettingsController : Controller
    {
        private readonly IDeletableEntityRepository<Setting> repository;

        public SettingsController(IDeletableEntityRepository<Setting> repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var settings = this.repository.All().To<SettingViewModel>().ToList();
            var model = new SettingsListViewModel { Settings = settings };
            return this.View(model);
        }

        public async Task<IActionResult> InsertSetting()
        {
            var random = new Random();
            var setting = new Setting { Name = $"Name_{random.Next()}", Value = $"Value_{random.Next()}" };
            this.repository.Add(setting);

            await this.repository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
