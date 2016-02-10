using MvcTemplate.Services.Web;
using System.Web.Mvc;

namespace MvcTemplate.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }
    }
}
