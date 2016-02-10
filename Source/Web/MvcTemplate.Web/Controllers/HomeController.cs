namespace MvcTemplate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using MvcTemplate.Data;
    using MvcTemplate.Data.Models;

    public class HomeController : Controller
    {
        private IService service;

        public HomeController(IService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            this.service.Work();
            var db = new ApplicationDbContext();
            var usersCount = db.Users.Count();
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}
