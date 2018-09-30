namespace AspNetCoreTemplate.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;

    public class DeletePersonalDataInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
