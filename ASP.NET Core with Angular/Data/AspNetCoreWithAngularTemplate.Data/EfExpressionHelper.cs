namespace AspNetCoreTemplate.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore.Storage;

    internal static class EfExpressionHelper
    {
        private static readonly Type StringType = typeof(string);
        private static readonly MethodInfo ValueBufferGetValueMethod =
            typeof(ValueBuffer).GetRuntimeProperties().Single(p => p.GetIndexParameters().Any()).GetMethod;

        private static readonly MethodInfo EfPropertyMethod =
            typeof(EF).GetTypeInfo().GetDeclaredMethod(nameof(Property));

        public static Expression<Func<TEntity, bool>> BuildByIdPredicate<TEntity>(
            DbContext dbContext,
            object[] id)
            where TEntity : class
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entityType = typeof(TEntity);

            var entityParameter = Expression.Parameter(entityType, "e");

            var keyProperties = dbContext.Model.FindEntityType(entityType).FindPrimaryKey().Properties;

            var predicate = BuildPredicate(keyProperties, new ValueBuffer(id), entityParameter);

            return Expression.Lambda<Func<TEntity, bool>>(predicate, entityParameter);
        }

        private static BinaryExpression BuildPredicate(
            IReadOnlyList<IProperty> keyProperties,
            ValueBuffer keyValues,
            ParameterExpression entityParameter)
        {
            var keyValuesConstant = Expression.Constant(keyValues);

            BinaryExpression predicate = null;
            for (var i = 0; i < keyProperties.Count; i++)
            {
                var property = keyProperties[i];
                var equalsExpression =
                    Expression.Equal(
                        Expression.Call(
                            EfPropertyMethod.MakeGenericMethod(property.ClrType),
                            entityParameter,
                            Expression.Constant(property.Name, StringType)),
                        Expression.Convert(
                            Expression.Call(
                                keyValuesConstant,
                                ValueBufferGetValueMethod,
                                Expression.Constant(i)),
                            property.ClrType));

                predicate = predicate == null ? equalsExpression : Expression.AndAlso(predicate, equalsExpression);
            }

            return predicate;
        }
    }
}
