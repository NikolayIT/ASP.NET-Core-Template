namespace AspNetCoreWithAngularTemplate.Data.Models
{
    using AspNetCoreWithAngularTemplate.Data.Common.Models;

    public class Setting : BaseModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
