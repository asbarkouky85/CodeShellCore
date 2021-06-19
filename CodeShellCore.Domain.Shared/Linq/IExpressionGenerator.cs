using CodeShellCore.Linq.Filtering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Linq
{
    public interface IExpressionGenerator<T> where T : class
    {
        List<Expression> ToFilterExpressions(IEnumerable<PropertyFilter> f);
        List<Expression<Func<T, bool>>> ToStrongExpressions(IEnumerable<PropertyFilter> f);
    }
}
