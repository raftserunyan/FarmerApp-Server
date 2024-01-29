﻿using System.Linq.Expressions;

namespace FarmerApp.Data.Specifications.Common
{
    public interface ICommonSpecification<TEntity>
    {
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

        Expression<Func<TEntity, bool>> Criteria { get; }

        List<Expression<Func<TEntity, object>>> Includes { get; }

        List<string> IncludeStrings { get; }

        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
        Expression<Func<TEntity, object>> GroupBy { get; }

    }
}