using CodeShellCore.Data.Lookups;
using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Linq.Filtering
{
    public class FilterCollection
    {
        public string SearchTerm { get; set; }
        public IEnumerable<PropertyFilter> Filters { get; set; }

        public IEnumerable<Expression<Func<T, bool>>> GetFiltersFor<T>() where T : class
        {
            var gen = new ExpressionGenerator<T>();

            List<Expression<Func<T, bool>>> opts = gen.ToStrongExpressions(Filters);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                var ex = ExpressionStore.GetSearchExpression<T>(SearchTerm);
                if (ex != null)
                {
                    opts.Add(ex);
                }
                else if (typeof(T).Implements(typeof(INamed)))
                {
                    Expression<Func<T, bool>> iex = e => ((INamed)e).Name.Contains(SearchTerm);
                    opts.Add(iex);
                }
            }
            return opts;
        }
    }
}
