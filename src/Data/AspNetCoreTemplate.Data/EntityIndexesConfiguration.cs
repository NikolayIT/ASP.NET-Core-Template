namespace AspNetCoreTemplate.Data
{
    using System.Linq;

    using AspNetCoreTemplate.Data.Common.Models;

    using Microsoft.EntityFrameworkCore;

    internal static class EntityIndexesConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
            => modelBuilder.Model
                .GetEntityTypes()
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType))
                .ToList()
                .ForEach(deletableEntityType =>
                    modelBuilder
                        .Entity(deletableEntityType.ClrType)
                        .HasIndex(nameof(IDeletableEntity.IsDeleted)));
    }
}
