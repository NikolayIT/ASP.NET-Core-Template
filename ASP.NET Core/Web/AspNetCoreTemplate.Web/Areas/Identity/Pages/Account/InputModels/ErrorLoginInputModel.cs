namespace AspNetCoreTemplate.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;

    public class ErrorLoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
