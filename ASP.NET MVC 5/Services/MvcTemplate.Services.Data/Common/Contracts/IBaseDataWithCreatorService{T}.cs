namespace MvcTemplate.Services.Data.Common.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MvcTemplate.Data.Common.Models;

    public interface IBaseDataWithCreatorService<T> : IBaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo, IEntityWithCreator
    {
        void Delete(object id, string userId);

        IQueryable<T> GetAllByUser(string userId);
    }
}
