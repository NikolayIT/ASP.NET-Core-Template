namespace AspNetCoreTemplate.Web.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Web.Areas.Identity.Pages.Account.InputModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class ResetPasswordModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public ResetPasswordInputModel Input { get; set; }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return this.BadRequest("A code must be supplied for password reset.");
            }

            this.Input = new ResetPasswordInputModel
            {
                Code = code,
            };

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var user = await this.userManager.FindByEmailAsync(this.Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return this.RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await this.userManager.ResetPasswordAsync(user, this.Input.Code, this.Input.Password);
            if (result.Succeeded)
            {
                return this.RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.Page();
        }
    }
}
