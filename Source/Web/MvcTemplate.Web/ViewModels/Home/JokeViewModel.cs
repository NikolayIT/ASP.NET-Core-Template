using MvcTemplate.Data.Models;
using MvcTemplate.Web.Infrastructure.Mapping;

namespace MvcTemplate.Web.ViewModels.Home
{
    public class JokeViewModel : IMapFrom<Joke>, IMapTo<Joke>
    {
        public string Content { get; set; }
    }
}
