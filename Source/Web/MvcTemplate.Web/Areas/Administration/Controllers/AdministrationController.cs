namespace MvcTemplate.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using MvcTemplate.Common;
    using MvcTemplate.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
