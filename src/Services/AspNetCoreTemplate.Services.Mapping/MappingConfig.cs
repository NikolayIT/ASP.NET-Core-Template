namespace AspNetCoreTemplate.Services.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Mapster;

    public static class MappingConfig
    {
        private static bool initialized;

        public static TypeAdapterConfig GlobalConfig { get; private set; }

        public static MappingAdapter MapperInstance { get; private set; }

        public static void RegisterMappings(params Assembly[] assemblies)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            var types = assemblies.SelectMany(a => a.GetExportedTypes()).ToList();

            var config = new TypeAdapterConfig();

            foreach (var map in GetFromMaps(types))
            {
                config.NewConfig(map.Source, map.Destination);
            }

            foreach (var map in GetToMaps(types))
            {
                config.NewConfig(map.Source, map.Destination);
            }

            foreach (var map in GetCustomMappings(types))
            {
                map.CreateMappings(config);
            }

            GlobalConfig = config;
            MapperInstance = new MappingAdapter(config);
        }

        private static IEnumerable<TypesMap> GetFromMaps(IEnumerable<Type> types)
        {
            var fromMaps = from t in types
                           from i in t.GetTypeInfo().GetInterfaces()
                           where i.GetTypeInfo().IsGenericType &&
                                 i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                                 !t.GetTypeInfo().IsAbstract &&
                                 !t.GetTypeInfo().IsInterface
                           select new TypesMap
                           {
                               Source = i.GetTypeInfo().GetGenericArguments()[0],
                               Destination = t,
                           };

            return fromMaps;
        }

        private static IEnumerable<TypesMap> GetToMaps(IEnumerable<Type> types)
        {
            var toMaps = from t in types
                         from i in t.GetTypeInfo().GetInterfaces()
                         where i.GetTypeInfo().IsGenericType &&
                               i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                               !t.GetTypeInfo().IsAbstract &&
                               !t.GetTypeInfo().IsInterface
                         select new TypesMap
                         {
                             Source = t,
                             Destination = i.GetTypeInfo().GetGenericArguments()[0],
                         };

            return toMaps;
        }

        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            var customMaps = from t in types
                             from i in t.GetTypeInfo().GetInterfaces()
                             where typeof(IHaveCustomMappings).GetTypeInfo().IsAssignableFrom(t) &&
                                   !t.GetTypeInfo().IsAbstract &&
                                   !t.GetTypeInfo().IsInterface
                             select (IHaveCustomMappings)Activator.CreateInstance(t);

            return customMaps;
        }

        public sealed class MappingAdapter
        {
            private readonly TypeAdapterConfig config;

            public MappingAdapter(TypeAdapterConfig config)
            {
                this.config = config;
            }

            public TDestination Map<TDestination>(object source)
            {
                return source.Adapt<TDestination>(this.config);
            }

            public TDestination Map<TSource, TDestination>(TSource source)
            {
                return source.Adapt<TSource, TDestination>(this.config);
            }

            public void Map<TSource, TDestination>(TSource source, TDestination destination)
            {
                source.Adapt(destination, this.config);
            }
        }

        private class TypesMap
        {
            public Type Source { get; set; }

            public Type Destination { get; set; }
        }
    }
}
