namespace AspNetCoreTemplate.Web.ViewModels.Settings
{
    using System.Collections.Generic;

    public class SettingsListViewModel
    {
        public IEnumerable<SettingViewModel> Settings { get; set; }
    }
}
