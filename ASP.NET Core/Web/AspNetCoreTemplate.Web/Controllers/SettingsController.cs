namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Linq;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Web.Infrastructure.Mapping;
    using AspNetCoreTemplate.Web.Models.Settings;

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
    }
}
