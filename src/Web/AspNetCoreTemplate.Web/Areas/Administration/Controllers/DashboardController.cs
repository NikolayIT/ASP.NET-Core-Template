namespace AspNetCoreTemplate.Web.Areas.Administration.Controllers
{
    using AspNetCoreTemplate.Services.Data;
    using AspNetCoreTemplate.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController(ISettingsService settingsService) : AdministrationController
    {
        private readonly ISettingsService settingsService = settingsService;

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
