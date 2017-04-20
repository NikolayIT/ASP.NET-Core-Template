namespace AspNetCoreTemplate.Data.Models
{
    using System;

    using AspNetCoreTemplate.Data.Common.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
