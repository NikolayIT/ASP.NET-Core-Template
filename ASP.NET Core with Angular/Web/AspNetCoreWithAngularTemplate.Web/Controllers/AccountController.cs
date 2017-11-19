namespace AspNetCoreWithAngularTemplate.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreWithAngularTemplate.Data.Models;
    using AspNetCoreWithAngularTemplate.Web.ViewModels.Account;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRegisterBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                var firstError = this.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).FirstOrDefault();
                return this.BadRequest(firstError);
            }

            var user = new ApplicationUser { Email = model.Email, UserName = model.Email };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors.Select(e => e.Description).FirstOrDefault());
        }
    }
}
