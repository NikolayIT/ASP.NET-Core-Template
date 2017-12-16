namespace AspNetCoreWithAngularTemplate.Web.ViewModels.TodoItems
{
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreWithAngularTemplate.Common.Mapping;
    using AspNetCoreWithAngularTemplate.Data.Models;

    public class TodoItemBindingModel : IMapTo<TodoItem>
    {
        [Required]
        public string Title { get; set; }
    }
}
