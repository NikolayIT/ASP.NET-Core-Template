namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Repositories;
    using AspNetCoreTemplate.Services.Mapping;

    using Microsoft.EntityFrameworkCore;

    using Xunit;

    public class MappingTests
    {
        public MappingTests()
        {
            MappingConfig.RegisterMappings(typeof(MappingTests).Assembly);
        }

        [Fact]
        public async Task GetAllShouldProjectEntitiesUsingMapFromAndCustomMappings()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MappingTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Settings.Add(new Setting { Name = "Theme", Value = "Dark" });
            dbContext.Settings.Add(new Setting { Name = "Language", Value = "en" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Setting>(dbContext);
            var service = new SettingsService(repository);
            var settings = service.GetAll<SettingTestViewModel>().OrderBy(x => x.Name).ToList();

            Assert.Equal(2, settings.Count);
            Assert.Equal("Language", settings[0].Name);
            Assert.Equal("Language = en", settings[0].NameAndValue);
            Assert.Equal("Theme = Dark", settings[1].NameAndValue);
        }

        [Fact]
        public void MapperShouldMapUsingMapFromAndCustomMappings()
        {
            var setting = new Setting { Id = 42, Name = "Theme", Value = "Dark" };

            var viewModel = MappingConfig.MapperInstance.Map<SettingTestViewModel>(setting);

            Assert.Equal(42, viewModel.Id);
            Assert.Equal("Theme", viewModel.Name);
            Assert.Equal("Theme = Dark", viewModel.NameAndValue);
        }

        [Fact]
        public void MapperShouldMapUsingMapTo()
        {
            var inputModel = new SettingTestInputModel { Name = "Theme", Value = "Light" };

            var setting = MappingConfig.MapperInstance.Map<SettingTestInputModel, Setting>(inputModel);

            Assert.Equal("Theme", setting.Name);
            Assert.Equal("Light", setting.Value);
        }

        [Fact]
        public void MapperShouldUpdateExistingDestinationAndKeepUnmappedMembers()
        {
            var inputModel = new SettingTestInputModel { Name = "Theme", Value = "Light" };
            var setting = new Setting { Id = 7, Name = "Old", Value = "Dark", IsDeleted = true };

            MappingConfig.MapperInstance.Map(inputModel, setting);

            Assert.Equal(7, setting.Id);
            Assert.True(setting.IsDeleted);
            Assert.Equal("Theme", setting.Name);
            Assert.Equal("Light", setting.Value);
        }

        public class SettingTestViewModel : IMapFrom<Setting>, IHaveCustomMappings
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string NameAndValue { get; set; }

            public void CreateMappings(Mapster.TypeAdapterConfig configuration)
            {
                configuration.NewConfig<Setting, SettingTestViewModel>()
                    .Map(m => m.NameAndValue, x => x.Name + " = " + x.Value);
            }
        }

        public class SettingTestInputModel : IMapTo<Setting>
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}
