namespace AspNetCoreTemplate.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;

    public class EnableAuthenticatorInputModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Verification Code")]
        public string Code { get; set; }
    }
}
