﻿using System.Linq.Expressions;

namespace FarmerApp.Core.Query.DynamicFilterBuilder.Builder.Internal.OperationalQueryBuilders;

internal class GreaterThanQueryBuilder : IOperationalQueryBuilder
{
    public static readonly GreaterThanQueryBuilder Instance = new();

    private GreaterThanQueryBuilder()
    {
    }

    public Expression Build(Type propertyType, Expression propertyExpression, string filterValue)
    {
        var filterValueExpression = GetValueExpression(propertyType, filterValue);

        var result = Expression.GreaterThan(propertyExpression, filterValueExpression);

        return result;
    }

    private static ConstantExpression GetValueExpression(Type type, string valueString)
    {
        if (type == typeof(int))
            return Expression.Constant(Convert.ToInt32(valueString), typeof(int));
        if (type == typeof(DateTime))
            return Expression.Constant(Convert.ToDateTime(valueString), typeof(DateTime));
        if (type == typeof(DateTimeOffset))
            return Expression.Constant(new DateTimeOffset(Convert.ToDateTime(valueString), TimeSpan.Zero), typeof(DateTimeOffset));
        if (type == typeof(double))
            return Expression.Constant(Convert.ToDouble(valueString), typeof(double));

        throw new NotSupportedException();
    }
}