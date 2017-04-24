namespace AspNetCoreTemplate.Web.Models.Settings
{
    using System.Collections.Generic;

    public class SettingsListViewModel
    {
        public IEnumerable<SettingViewModel> Settings { get; set; }
    }
}
