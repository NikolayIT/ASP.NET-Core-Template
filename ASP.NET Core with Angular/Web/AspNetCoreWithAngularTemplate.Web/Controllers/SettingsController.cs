namespace AspNetCoreWithAngularTemplate.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreWithAngularTemplate.Data.Common.Repositories;
    using AspNetCoreWithAngularTemplate.Data.Models;
    using AspNetCoreWithAngularTemplate.Web.Infrastructure.Mapping;
    using AspNetCoreWithAngularTemplate.Web.ViewModels.Settings;

    using Microsoft.AspNetCore.Mvc;

    public class SettingsController : BaseController
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
