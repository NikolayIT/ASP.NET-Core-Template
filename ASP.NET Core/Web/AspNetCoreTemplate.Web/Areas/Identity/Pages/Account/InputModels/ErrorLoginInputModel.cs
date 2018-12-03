namespace AspNetCoreTemplate.Web.Areas.Identity.Pages.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ErrorLoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
