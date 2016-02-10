using MvcTemplate.Data.Common;
using MvcTemplate.Data.Models;
using System;
using System.Linq;

namespace MvcTemplate.Services.Data
{
    public class JokesService : IJokesService
    {
        private IDbRepository<Joke> jokes;

        public JokesService()
        {
        }

        public JokesService(IDbRepository<Joke> jokes)
        {
            this.jokes = jokes;
        }

        public IQueryable<Joke> GetRandomJokes(int count)
        {
            return this.jokes.All()
                .OrderBy(x => Guid.NewGuid())
                .Take(count);
        }
    }
}
