namespace MvcTemplate.Services.Data.Common
{
    using System;
    using System.Linq;

    using MvcTemplate.Common;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Common.Models;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Data.Common.Contracts;

    public class BaseDataWithCreatorService<T> : BaseDataService<T>, IBaseDataWithCreatorService<T>
        where T : class, IDeletableEntity, IAuditInfo, IEntityWithCreator
    {
        public BaseDataWithCreatorService(IDbRepository<T> dataSet, IDbRepository<ApplicationUser> users)
            : base(dataSet)
        {
            this.Users = users;
        }

        protected IDbRepository<ApplicationUser> Users { get; set; }

        public IQueryable<T> GetAllByUser(string userId)
        {
            return this.Data
                .All()
                .Where(x => x.UserId == userId);
        }

        public void Delete(object id, string userId)
        {
            var user = this.Users.GetById(userId);
            var isAdmin = user.Roles.Any(x => x.RoleId == GlobalConstants.AdministratorRoleName);
            var training = this.Data.GetById(id);
            if (training == null)
            {
                throw new InvalidOperationException($"No entity with provided id ({id}) found.");
            }

            if (training.UserId != userId && !isAdmin)
            {
                throw new InvalidOperationException("Cannot delete entity. Unauthorized request.");
            }

            this.Data.Delete(training);
            this.Data.Save();
        }
    }
}
