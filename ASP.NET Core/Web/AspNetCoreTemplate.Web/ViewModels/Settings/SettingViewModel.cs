namespace AspNetCoreTemplate.Web.ViewModels.Settings
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Web.Infrastructure.Mapping;

    public class SettingViewModel : IMapFrom<Setting>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
