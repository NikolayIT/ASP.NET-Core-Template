namespace AspNetCoreTemplate.Web.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
