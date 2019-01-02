namespace AspNetCoreTemplate.Web.Areas.Administration.Controllers
{
    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
