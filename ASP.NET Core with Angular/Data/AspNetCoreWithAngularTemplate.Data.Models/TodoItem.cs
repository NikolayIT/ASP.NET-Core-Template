namespace AspNetCoreWithAngularTemplate.Data.Models
{
    using AspNetCoreWithAngularTemplate.Data.Common.Models;

    public class TodoItem : BaseModel<int>
    {
        public string Title { get; set; }

        public bool IsDone { get; set; }
    }
}
