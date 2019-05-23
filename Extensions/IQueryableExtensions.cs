using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AspAng.Core.Models;

namespace AspAng.Extensions
{
    public static class IQueryableExtensions
    {
         public static IQueryable<T> ApplySorting<T> (this IQueryable<T> query, IQueryObject queryObj , Dictionary<string, Expression<Func<T, object>> > ColumnsMap) {

        if(String.IsNullOrWhiteSpace(queryObj.SortBy) || !ColumnsMap.ContainsKey(queryObj.SortBy))
        
        return query;

         query = queryObj.IsSortAscending ? query.OrderBy (ColumnsMap[queryObj.SortBy]) : query.OrderByDescending (ColumnsMap[queryObj.SortBy]);
         
         return query;

        }
        
    }
}