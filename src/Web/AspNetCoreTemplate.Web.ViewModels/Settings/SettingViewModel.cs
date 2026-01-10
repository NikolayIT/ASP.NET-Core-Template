namespace AspNetCoreTemplate.Web.ViewModels.Settings
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class SettingViewModel : IMapFrom<Setting>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string NameAndValue { get; set; }

        public void CreateMappings(Mapster.TypeAdapterConfig configuration)
        {
            configuration.NewConfig<Setting, SettingViewModel>()
                .Map(m => m.NameAndValue, x => x.Name + " = " + x.Value);
        }
    }
}
