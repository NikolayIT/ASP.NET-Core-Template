namespace AspNetCoreTemplate.Services.Data
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Services.ServiceLifetimes;

    public interface ISettingsService : ITransientService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
