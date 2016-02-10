using MvcTemplate.Data.Models;
using System.Linq;
using System;
using MvcTemplate.Data.Common;

namespace MvcTemplate.Services.Data
{
    public interface IJokesService
    {
        IQueryable<Joke> GetRandomJokes(int count);
    }
}
