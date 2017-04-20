namespace AspNetCoreTemplate.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
