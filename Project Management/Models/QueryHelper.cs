using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Project_Management.Models
{
	public static class QueryHelper
	{
        public static async Task<Models.Dtos.Paginated<T>> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Dtos.Paginated<T>(items, count, pageNumber, pageSize);
        }

        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool descending = false)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            var property = type.GetProperty(propertyName);
            if (property == null) throw new ArgumentException($"Property {propertyName} not found in {type}");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var methodName = descending ? "OrderByDescending" : "OrderBy";

            return source.Provider.CreateQuery<T>(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new Type[] { type, property.PropertyType },
                    source.Expression,
                    Expression.Quote(orderByExp)
                )
            );
        }

        public static IQueryable<T> SearchByProperty<T>(this IQueryable<T> source, string propertyName, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return source;
            }

            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            var property = type.GetProperty(propertyName);
            if (property == null) throw new ArgumentException($"Property {propertyName} not found in {type}");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var toLowerMethodInfo = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var searchTermLower = searchTerm.ToLower();
            var searchTermExpression = Expression.Constant(searchTermLower);
            var propertyToLowerExpression = Expression.Call(propertyAccess, toLowerMethodInfo);
            var containsMethodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsExpression = Expression.Call(propertyToLowerExpression, containsMethodInfo, searchTermExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);

            return source.Where(lambda);
        }
    }
}

