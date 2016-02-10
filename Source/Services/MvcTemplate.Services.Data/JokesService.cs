using MvcTemplate.Data.Common;
using MvcTemplate.Data.Models;
using MvcTemplate.Services.Web;
using System;
using System.Linq;

namespace MvcTemplate.Services.Data
{
    public class JokesService : IJokesService
    {
        private IDbRepository<Joke> jokes;
        private IIdentifierProvider identifierProvider;

        public JokesService(IDbRepository<Joke> jokes,
            IIdentifierProvider identifierProvider)
        {
            this.jokes = jokes;
            this.identifierProvider = identifierProvider;
        }

        public Joke GetById(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var joke = this.jokes.GetById(intId);
            return joke;
        }

        public IQueryable<Joke> GetRandomJokes(int count)
        {
            return this.jokes.All()
                .OrderBy(x => Guid.NewGuid())
                .Take(count);
        }
    }
}
