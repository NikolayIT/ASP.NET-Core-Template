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

    public class HomeController : Controller
    {
        private IJokesService jokes;
        private ICategoriesService jokeCategories;

        public HomeController(
            IJokesService jokes,
            ICategoriesService jokeCategories)
        {
            this.jokes = jokes;
            this.jokeCategories = jokeCategories;
        }

        public ActionResult Index()
        {
            var jokes = this.jokes.GetRandomJokes(3).To<JokeViewModel>().ToList();
            var categories = this.jokeCategories.GetAll().To<JokeCategoryViewModel>().ToList();
            var viewModel = new IndexViewModel
            {
                Jokes = jokes,
                Categories = categories
            };

            return this.View(viewModel);
        }
    }
}
