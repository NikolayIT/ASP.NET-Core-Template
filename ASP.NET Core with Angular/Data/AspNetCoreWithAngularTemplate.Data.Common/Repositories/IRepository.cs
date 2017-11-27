namespace AspNetCoreWithAngularTemplate.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task<TEntity> GetByIdAsync(params object[] id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Detach(TEntity entity);

        void DetachAll();

        void RunQuery(string query, params object[] parameters);

        Task<int> SaveChangesAsync();
    }
}
