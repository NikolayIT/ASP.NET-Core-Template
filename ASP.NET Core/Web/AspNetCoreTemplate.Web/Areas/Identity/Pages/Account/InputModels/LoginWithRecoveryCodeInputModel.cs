namespace AspNetCoreTemplate.Web.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;

    public class LoginWithRecoveryCodeInputModel
    {
        [BindProperty]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}
