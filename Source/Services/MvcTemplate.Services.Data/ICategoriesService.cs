using MvcTemplate.Data.Models;
using System.Linq;
using System;
using MvcTemplate.Data.Common;

namespace MvcTemplate.Services.Data
{
    public interface ICategoriesService
    {
        IQueryable<JokeCategory> GetAll();
    }

    public class CategoriesService : ICategoriesService
    {
        IDbRepository<JokeCategory> categories;
        public CategoriesService(IDbRepository<JokeCategory> categories)
        {
            this.categories = categories;
        }

        public IQueryable<JokeCategory> GetAll()
        {
            return this.categories.All().OrderBy(x => x.Name);
        }
    }
}
