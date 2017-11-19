namespace AspNetCoreWithAngularTemplate.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreWithAngularTemplate.Data.Common.Models;

    public class TodoItem : BaseModel<int>
    {
        [Required]
        public string Title { get; set; }

        public bool IsDone { get; set; }
    }
}
