namespace AspNetCoreTemplate.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Web.Areas.Administration.ViewModels.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public DashboardController(
            IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsRepository.All().Count(), };
            return this.View(viewModel);
        }
    }
}
