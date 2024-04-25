using CodeShellCore.Data;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Linq
{
    public static class AppExpressions
    {
        public static IEnumerable<Expression<Func<T, bool>>> GetFilters<T>(IEnumerable<PropertyFilterDto> filters) where T : class
        {
            return Expressions.ToStrongExpressions<T>(filters);
        }

        public static List<Expression> ToFilterExpressions<T>(IEnumerable<IPropertyFilter> fs) where T : class
        {
            List<Expression> exs = new List<Expression>();
            foreach (IPropertyFilter f in fs)
            {
                var ex = Expressions.GetExpression<T>(f);
                if (ex != null)
                    exs.Add(ex);
            }

            return exs;
        }

        
    }
}
