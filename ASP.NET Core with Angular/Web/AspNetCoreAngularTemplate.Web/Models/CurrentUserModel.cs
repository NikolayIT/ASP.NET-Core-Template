namespace AspNetCoreAngularTemplate.Web.Models
{
    using System.Collections.Generic;

    public class CurrentUserModel
    {
        public CurrentUserModel()
        {
            this.Roles = new List<string>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
