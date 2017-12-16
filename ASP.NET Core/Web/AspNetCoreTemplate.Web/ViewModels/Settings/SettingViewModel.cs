namespace AspNetCoreTemplate.Web.ViewModels.Settings
{
    using AspNetCoreTemplate.Common.Mapping;
    using AspNetCoreTemplate.Data.Models;

    public class SettingViewModel : IMapFrom<Setting>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
