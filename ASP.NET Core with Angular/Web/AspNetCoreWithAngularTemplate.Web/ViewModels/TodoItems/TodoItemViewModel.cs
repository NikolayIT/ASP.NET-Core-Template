namespace AspNetCoreWithAngularTemplate.Web.ViewModels.TodoItems
{
    using AspNetCoreWithAngularTemplate.Common.Mapping;
    using AspNetCoreWithAngularTemplate.Data.Models;

    public class TodoItemViewModel : IMapFrom<TodoItem>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }
    }
}
