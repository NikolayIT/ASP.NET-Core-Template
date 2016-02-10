namespace MvcTemplate.Web.Controllers
{
    using System.Web.Mvc;

    using MvcTemplate.Services.Web;

    public abstract class BaseController : Controller
    {
        protected ICacheService Cache { get; set; }
    }
}
