using System.Collections.Generic;

namespace MvcTemplate.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<JokeViewModel> Jokes { get; set; }

        public IEnumerable<JokeCategoryViewModel> Categories { get; set; }
    }
}