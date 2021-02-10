using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq.Filtering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Linq
{
    public interface IExpressionGenerator<T> where T : class
    {
        List<Expression> ToFilterExpressions(IEnumerable<PropertyFilter> f);
        List<Expression<Func<T, bool>>> ToStrongExpressions(IEnumerable<PropertyFilter> f);
    }
}
