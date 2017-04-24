namespace AspNetCoreAngularTemplate.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;

    using AspNetCoreAngularTemplate.Services.Data;
    using AspNetCoreAngularTemplate.Web.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected readonly IUsersService UsersService;

        private readonly IHttpContextAccessor httpContextAccessor;

        protected BaseController(IHttpContextAccessor httpContextAccessor, IUsersService usersService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.UsersService = usersService;
            this.GetCurrentUser();
        }

        protected CurrentUserModel CurrentUser { get; private set; }

        protected string GetFirstModelStateError()
            => this.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault();

        private void GetCurrentUser()
        {
            var userId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                this.CurrentUser = new CurrentUserModel();
                return;
            }

            var username = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            this.CurrentUser = new CurrentUserModel { Id = userId, Username = username };
            this.CurrentUser.Roles = this.UsersService.GetUserRoles(this.CurrentUser.Id);
        }
    }
}
