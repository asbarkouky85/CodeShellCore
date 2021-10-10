using CodeShellCore.Linq;
using CodeShellCore.Linq.Filtering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore
{
    public static class DomainUtils
    {
        public static List<Expression<Func<T, bool>>> GetFilters<T>(IEnumerable<PropertyFilter> fs) where T : class
        {
            return new ExpressionGenerator<T>().ToStrongExpressions(fs);
        }
    }
}
