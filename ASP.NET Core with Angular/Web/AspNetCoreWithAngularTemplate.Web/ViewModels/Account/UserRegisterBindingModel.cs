namespace AspNetCoreWithAngularTemplate.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(6)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; }
    }
}
