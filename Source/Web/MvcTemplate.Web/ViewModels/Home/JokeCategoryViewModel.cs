using MvcTemplate.Data.Models;
using MvcTemplate.Web.Infrastructure.Mapping;

namespace MvcTemplate.Web.ViewModels.Home
{
    public class JokeCategoryViewModel : IMapFrom<JokeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}