namespace MvcTemplate.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using MvcTemplate.Data;
    using MvcTemplate.Data.Models;
    using Data.Common;
    using ViewModels.Home;
    using Infrastructure.Mapping;
    using Services.Data;
    using Services.Web;
    public class HomeController : Controller
    {
        private IJokesService jokes;
        private ICategoriesService jokeCategories;
        private ICacheService cacheService;

        public HomeController(
            IJokesService jokes,
            ICategoriesService jokeCategories,
            ICacheService cacheService)
        {
            this.jokes = jokes;
            this.jokeCategories = jokeCategories;
            this.cacheService = cacheService;
        }

        public ActionResult Index()
        {
            var jokes = this.jokes.GetRandomJokes(3).To<JokeViewModel>().ToList();
            var categories =
                this.cacheService.Get(
                    "categories",
                    () => this.jokeCategories.GetAll().To<JokeCategoryViewModel>().ToList(),
                    30 * 60);
            var viewModel = new IndexViewModel
            {
                Jokes = jokes,
                Categories = categories
            };

            return this.View(viewModel);
        }
    }
}
