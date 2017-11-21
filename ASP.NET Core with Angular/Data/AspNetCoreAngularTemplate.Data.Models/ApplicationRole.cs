﻿namespace AspNetCoreAngularTemplate.Data.Models
{
    using System;

    using AspNetCoreAngularTemplate.Data.Common.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
