namespace AspNetCoreAngularTemplate.Data.Common.Repositories
{
    using System.Linq;

    using AspNetCoreAngularTemplate.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
