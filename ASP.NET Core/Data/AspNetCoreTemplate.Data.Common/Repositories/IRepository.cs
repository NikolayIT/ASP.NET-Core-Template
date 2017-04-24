namespace AspNetCoreTemplate.Data.Common.Repositories
{
    using System;
    using System.Linq;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();

        TEntity GetById(params object[] id);

        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Save();
    }
}
