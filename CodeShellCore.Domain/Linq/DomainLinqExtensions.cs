using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Types;
using CodeShellCore.Linq.Filtering;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeShellCore.Linq
{
    public static class DomainLinqExtensions
    {
        public static IQueryable<T> PageUsing<T>(this IQueryable<T> q, PagedListRequest<T> opts) where T : class
        {
            if (opts == null)
                opts = new PagedListRequest<T>();

            if (opts.Filters != null)
            {
                for (int i = 0; i < opts.Filters.Count; i++)
                {
                    Expression<Func<T, bool>> e = (Expression<Func<T, bool>>)opts.Filters[i];
                    q = q.Where(e);
                }
            }

            if (!string.IsNullOrEmpty(opts.SearchTerm))
            {
                var ex = ExpressionStore.GetSearchExpression<T>(opts.SearchTerm);
                if (ex != null)
                {
                    q = q.Where(ex);
                }
                else if (typeof(T).Implements(typeof(INamed)))
                {
                    Expression<Func<T, bool>> iex = e => ((INamed)e).Name.Contains(opts.SearchTerm);
                    q = q.Where(iex);
                }
            }

            if (!string.IsNullOrEmpty(opts.OrderProperty))
                q = q.SortWith(opts.OrderProperty, opts.Direction);


            return q;
        }

        public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> q, PagedListRequest<T> opts) where T : class
        {
            PagedResult<T> res = new PagedResult<T>();
            q = q.PageUsing(opts);

            res.TotalCount = q.Count(d => true);

            if (opts.Showing > 0)
                q = q.Skip(opts.Skip).Take(opts.Showing);

            res.List = q.ToList();
            return res;
        }



        public static List<T> ToListWith<T>(this IQueryable<T> q, FilterCollection coll) where T : class
        {
            var fils = coll.GetFiltersFor<T>();
            foreach (var ex in fils)
                q = q.Where(ex);
            return q.ToList();
        }

        public static IQueryable<T> SortWith<T, TVal>(this IQueryable<T> q, Expression<Func<T, TVal>> exp, SortDir dir) where T : class
        {
            return (dir == SortDir.ASC) ? q.OrderBy(exp) : q.OrderByDescending(exp);
        }

        public static int Count<T>(this IQueryable<T> q, FilterCollection coll) where T : class
        {
            var fils = coll.GetFiltersFor<T>();
            foreach (var ex in fils)
                q = q.Where(ex);
            return q.Count(e => true);
        }

        
        public static List<T> ToListWith<T>(this IQueryable<T> q, PagedListRequest<T> opts) where T : class
        {
            if (opts.Filters != null)
            {
                for (int i = 0; i < opts.Filters.Count; i++)
                {
                    Expression<Func<T, bool>> e = (Expression<Func<T, bool>>)opts.Filters[i];
                    q = q.Where(e);
                }
            }

            if (!string.IsNullOrEmpty(opts.OrderProperty))
                q = q.SortWith(opts.OrderProperty, opts.Direction);

            if (opts.Showing > 0)
                q = q.Skip(opts.Skip).Take(opts.Showing);

            return q.ToList();

        }

        public static PagedListRequest<T> GetOptionsFor<T>(this PagedListRequest posted, bool ignoreFilters = false) where T : class
        {
            PagedListRequest<T> opts = new PagedListRequest<T>();
            opts.Skip = posted.Skip;
            opts.Showing = posted.Showing;
            opts.SearchTerm = posted.SearchTerm;

            opts.OrderProperty = posted.OrderProperty;

            opts.Filters = Expressions.ToFilterExpressions<T>(posted.PropertyFilters);

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

        public static IQueryable<T> SortWith<T>(this IQueryable<T> q, string propertyName, SortDir dir) where T : class
        {
            PropertyInfo prop = typeof(T).GetProperty(propertyName);

            if (prop == null)
                return q;

            if (prop.PropertyType == typeof(string))
            {
                var exp = Expressions.PropertyExpression<T, string>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else if (prop.PropertyType == typeof(double))
            {
                var exp = Expressions.PropertyExpression<T, double>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else if (prop.PropertyType == typeof(double?))
            {
                var exp = Expressions.PropertyExpression<T, double?>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else if (prop.PropertyType.IsDecimalType())
            {
                var exp = Expressions.PropertyExpression<T, decimal>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else if (prop.PropertyType.IsDecimalType(true))
            {
                var exp = Expressions.PropertyExpression<T, decimal?>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else if (prop.PropertyType.IsIntgerType())
            {
                var exp = Expressions.PropertyExpression<T, long>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else if (prop.PropertyType.IsIntgerType(true))
            {
                var exp = Expressions.PropertyExpression<T, long?>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else if (prop.PropertyType == typeof(DateTime))
            {
                var exp = Expressions.PropertyExpression<T, DateTime>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else if (prop.PropertyType == typeof(DateTime?))
            {
                var exp = Expressions.PropertyExpression<T, DateTime?>(propertyName);
                q = q.SortWith(exp, dir);
            }
            else
            {
                var exp = Expressions.Property<T>(propertyName);
                q = q.SortWith(exp, dir);
            }
            return q;
        }

        public static IQueryable<IGrouping<TRet, T>> GroupWith<T, TRet>(this IQueryable<T> q, string propertyName) where T : class
        {
            var ex = Expressions.PropertyExpression<T, TRet>(propertyName);
            if (ex == null)
                throw new Exception($"Property {propertyName} does not exist in type {typeof(T).GetType().FullName}");
            return q.GroupBy(ex);
        }

        public static IQueryable<IGrouping<TRet, T>> GroupWithObject<T, TRet>(this IQueryable<T> q, Dictionary<string, string> dic) where T : class where TRet : class
        {
            var ex = Expressions.ObjectMapping<T, TRet>(dic);
            return q.GroupBy(ex);
        }
    }
}
