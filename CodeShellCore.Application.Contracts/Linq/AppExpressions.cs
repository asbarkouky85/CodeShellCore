using CodeShellCore.Linq.Filtering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Linq
{
    public class AppExpressions
    {
        public static IEnumerable<Expression<Func<T, bool>>> GetFilters<T>(IEnumerable<PropertyFilter> filters) where T : class
        {
            return new ExpressionGenerator<T>().ToStrongExpressions(filters);
        }
    }
}
