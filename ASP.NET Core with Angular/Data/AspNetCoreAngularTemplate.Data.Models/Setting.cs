namespace AspNetCoreAngularTemplate.Data.Models
{
    using AspNetCoreAngularTemplate.Data.Common.Models;

    public class Setting : BaseModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
