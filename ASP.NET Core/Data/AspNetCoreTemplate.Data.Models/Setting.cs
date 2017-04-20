namespace AspNetCoreTemplate.Data.Models
{
    using AspNetCoreTemplate.Data.Common.Models;

    public class Setting : BaseModel<string>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
