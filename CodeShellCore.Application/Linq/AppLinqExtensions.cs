using CodeShellCore.Data.Lookups;
using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Linq
{
    public static class AppLinqExtensions
    {
        public static PagedListRequest<T> GetOptionsFor<T>(this PagedListRequestDto posted, bool ignoreFilters = false) where T : class
        {
            PagedListRequest<T> opts = new PagedListRequest<T>();
            opts.Skip = posted.Skip;
            opts.Showing = posted.Showing;
            opts.SearchTerm = posted.SearchTerm;
            SortDir dir;
            if (Enum.TryParse(posted.Direction, out dir))
                opts.Direction = dir;

            opts.OrderProperty = posted.OrderProperty;

            if (posted.Filters != null && !ignoreFilters)
            {
                opts.Filters = Expressions.ToFilterExpressions<T>(posted.PropertyFilters);
            }

            if (!string.IsNullOrEmpty(posted.SearchTerm))
            {
                var ex = ExpressionStore.GetSearchExpression<T>(posted.SearchTerm);
                if (ex != null)
                {
                    opts.AddFilter(ex);
                }
                else if (typeof(T).Implements(typeof(INamed)))
                {
                    Expression<Func<T, bool>> iex = e => ((INamed)e).Name.Contains(posted.SearchTerm);
                    opts.AddFilter(iex);
                }
            }
            return opts;
        }
    }
}
