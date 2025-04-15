namespace AspNetCoreTemplate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class SettingsService(IDeletableEntityRepository<Setting> settingsRepository) : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository = settingsRepository;

        public int GetCount() => this.settingsRepository.AllAsNoTracking().Count();

        public IEnumerable<T> GetAll<T>() => this.settingsRepository.All().To<T>().ToList();
    }
}
