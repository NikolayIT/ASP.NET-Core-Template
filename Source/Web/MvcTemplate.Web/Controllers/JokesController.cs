namespace MvcTemplate.Web.Controllers
{
    using System.Web.Mvc;

    using MvcTemplate.Services.Data;
    using MvcTemplate.Web.Infrastructure.Mapping;
    using MvcTemplate.Web.ViewModels.Home;
    using Services.Web;
    using ViewModels.Jokes;

    public class JokesController : BaseController
    {
        private readonly IJokesService jokes;

        public JokesController(
            IJokesService jokes)
        {
            this.jokes = jokes;
        }

        public ActionResult ById(string id)
        {
            var joke = this.jokes.GetById(id);
            var viewModel = this.Mapper.Map<JokeViewModel>(joke);
            return this.View(viewModel);
        }

        public ActionResult Edit(string id)
        {
            var entity = this.jokes.GetById(id);
            var model = this.Mapper.Map<JokeEditViewModel>(entity);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JokeEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var entity = this.jokes.GetById(model.EncodedId);
            this.Mapper.Map(model, entity);
            this.jokes.Save();
            //var entity = Mapper.Map<Joke>(model);

            return this.RedirectToAction("ById");
        }

    }
}
