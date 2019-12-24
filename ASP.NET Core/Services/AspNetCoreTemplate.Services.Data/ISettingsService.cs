namespace AspNetCoreTemplate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISettingsService
    {
        Task<int> GetCountAsync();

        IEnumerable<T> GetAll<T>();
    }
}
