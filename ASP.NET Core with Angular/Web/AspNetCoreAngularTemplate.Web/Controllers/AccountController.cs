namespace AspNetCoreAngularTemplate.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreAngularTemplate.Data.Models;
    using AspNetCoreAngularTemplate.Services.Data;
    using AspNetCoreAngularTemplate.Web.Models.Accounts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(
            IHttpContextAccessor httpContextAccessor,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
            : base(httpContextAccessor, usersService)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UsersService.CreateAsync(model.Email, model.Password);

                if (result.Succeeded)
                {
                    return this.Ok();
                }

                this.ModelState.AddModelError(string.Empty, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return this.BadRequest(this.GetFirstModelStateError());
        }
    }
}
