namespace AspNetCoreWithAngularTemplate.Web.ViewModels.Settings
{
    using AspNetCoreWithAngularTemplate.Data.Models;
    using AspNetCoreWithAngularTemplate.Web.Infrastructure.Mapping;

    public class SettingViewModel : IMapFrom<Setting>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
