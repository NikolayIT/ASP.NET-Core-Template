namespace AspNetCoreTemplate.Data.Common.Repositories
{
    using System.Linq;

    using AspNetCoreTemplate.Data.Common.Models;

    public interface IDeletableEntityRepository<T> : IRepository<T>
        where T : class, IAuditInfo, IDeletableEntity
    {
        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);
    }
}
