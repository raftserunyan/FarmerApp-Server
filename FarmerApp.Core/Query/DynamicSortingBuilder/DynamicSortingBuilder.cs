using System.Linq.Expressions;
using System.Reflection;

namespace FarmerApp.Core.Query.DynamicSortingBuilder;

public static class DynamicSortingBuilder
{
    public static Func<IQueryable<TSource>, IOrderedQueryable<TSource>> BuildOrderingFunc<TSource>(
        IEnumerable<OrderingItem> orderings)
    {
        Expression result = Expression.Empty();

        var sourceParameterExpression =
            Expression.Parameter(typeof(IQueryable<>).MakeGenericType(typeof(TSource)), "source");
        var localParameterExpression = Expression.Parameter(typeof(TSource), "x");

        var type = typeof(TSource);
        var typeProperties = type.GetProperties();

        var currentIndex = 1;

        foreach (var ordering in orderings)
        {
            MemberExpression memberExpression = null!;

            var currentTypeProperties = typeProperties;
            PropertyInfo propertyInfo = null!;
            foreach (var propertyName in ordering.Property.Split('.'))
            {
                propertyInfo = currentTypeProperties.FirstOrDefault(x => x.Name.ToLower() == propertyName.ToLower())!;

                currentTypeProperties = propertyInfo.PropertyType.GetProperties();

                Expression memberBefore = memberExpression == null ? localParameterExpression : memberExpression;

                memberExpression = Expression.PropertyOrField(memberBefore, propertyName);
            }

            var lambdaExpression = Expression.Lambda(memberExpression, localParameterExpression);
            Expression left;
            string methodName;
            if (currentIndex == 1)
            {
                methodName = ordering.IsAscending ? "OrderBy" : "OrderByDescending";
                left = sourceParameterExpression;
            }
            else
            {
                methodName = ordering.IsAscending ? "ThenBy" : "ThenByDescending";
                left = result;
            }

            result = Expression.Call(typeof(Queryable),
                methodName,
                new[] { type, propertyInfo.PropertyType },
                left, lambdaExpression
            );

            currentIndex++;
        }

        var lambda =
            Expression.Lambda<Func<IQueryable<TSource>, IOrderedQueryable<TSource>>>(result,
                sourceParameterExpression);
        return lambda.Compile();
    }
}