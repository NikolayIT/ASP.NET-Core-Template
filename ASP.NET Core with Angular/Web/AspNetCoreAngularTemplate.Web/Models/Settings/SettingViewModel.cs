namespace AspNetCoreAngularTemplate.Web.Models.Settings
{
    using AspNetCoreAngularTemplate.Data.Models;
    using AspNetCoreAngularTemplate.Web.Infrastructure.Mapping;

    public class SettingViewModel : IMapFrom<Setting>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
