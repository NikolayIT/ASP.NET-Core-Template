namespace MvcTemplate.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
