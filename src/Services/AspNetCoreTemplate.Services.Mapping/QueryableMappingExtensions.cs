namespace AspNetCoreTemplate.Services.Mapping
{
    using System;
    using System.Linq;

    using Mapster;

    public static class QueryableMappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(this IQueryable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectToType<TDestination>(MappingConfig.GlobalConfig);
        }
    }
}
