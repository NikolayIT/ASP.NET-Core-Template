namespace AspNetCoreTemplate.Services.Mapping
{
    using System;
    using System.Linq.Expressions;

    public static class ObjectMappingExtensions
    {
        public static TDestination To<TDestination>(
           this object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AutoMapperConfig.MapperInstance.Map<TDestination>(source);
        }
    }
}
