namespace AspNetCoreWithAngularTemplate.Web.ViewModels.TodoItems
{
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreWithAngularTemplate.Data.Models;
    using AspNetCoreWithAngularTemplate.Web.Infrastructure.Mapping;

    public class TodoItemBindingModel : IMapTo<TodoItem>
    {
        [Required]
        public string Title { get; set; }
    }
}
