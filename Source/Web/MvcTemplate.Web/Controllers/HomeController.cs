namespace MvcTemplate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using MvcTemplate.Data;
    using MvcTemplate.Data.Models;

    public class HomeController : Controller
    {
        // private IDbRepostiory<>

        public HomeController()
        {

        }


        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var usersCount = db.Users.Count();
            return this.View();
        }
    }
}
