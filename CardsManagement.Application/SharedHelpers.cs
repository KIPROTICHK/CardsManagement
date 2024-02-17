using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CardsManagement.Application
{
    public static class SharedHelpers
    {
        public static bool StringCompare(this string b, string str)
        {
            if (string.IsNullOrEmpty(b) || string.IsNullOrEmpty(str))
            {
                return false;
            }

            if (b.Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> whereClause)
        {
            if (condition)
            {
                return query.Where(whereClause);
            }

            return query;
        }

        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortColumn, string SortColumnDirection)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
            string methodName = "OrderBy";
            if (SortColumnDirection.StringCompare("desc"))
            {
                methodName = "OrderByDescending";
            }

            PropertyInfo property = typeof(T).GetProperty(sortColumn);
            LambdaExpression expression = Expression.Lambda(Expression.MakeMemberAccess(parameterExpression, property), parameterExpression);
            Expression expression2 = Expression.Call(typeof(Queryable), methodName, new Type[2]
            {
            typeof(T),
            property.PropertyType
            }, query.Expression, Expression.Quote(expression));
            return query.Provider.CreateQuery<T>(expression2);
        }
        public static T CastMyObject<T>(this object j, HashSet<string> ignoreProperties = null)
        {
            if (j == null)
            {
                return default(T);
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            if (ignoreProperties != null)
            {
                jsonSerializerSettings.ContractResolver = new JsonIgnoreResolver(ignoreProperties);
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(j, jsonSerializerSettings), jsonSerializerSettings);
        }
       
    }
}
