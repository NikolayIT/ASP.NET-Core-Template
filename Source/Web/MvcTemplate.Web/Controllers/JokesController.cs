using MvcTemplate.Services.Data;
using MvcTemplate.Services.Web;
using MvcTemplate.Web.Infrastructure.Mapping;
using MvcTemplate.Web.ViewModels.Home;
using System.Web.Mvc;

namespace MvcTemplate.Web.Controllers
{
    public class JokesController : BaseController
    {
        private IIdentifierProvider identifierProvider;
        private IJokesService jokes;

        public JokesController(
            IIdentifierProvider identifierProvider,
            IJokesService jokes)
        {
            this.identifierProvider = identifierProvider;
            this.jokes = jokes;
        }

        public ActionResult ById(string id)
        {
            var joke = this.jokes.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<JokeViewModel>(joke);
            return this.View(viewModel);
        }
    }
}