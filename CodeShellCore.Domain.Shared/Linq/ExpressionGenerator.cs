using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Helpers;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.Types;

namespace CodeShellCore.Linq
{
    public class ExpressionGenerator<T> : IExpressionGenerator<T> where T : class
    {

        public IQueryable<T> SortWith(IQueryable<T> q, string propertyName, SortDir dir)
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

        public IQueryable<IGrouping<TRet, T>> GroupWith<TRet>(IQueryable<T> q, string propertyName)
        {
            var ex = Expressions.PropertyExpression<T, TRet>(propertyName);
            if (ex == null)
                throw new Exception($"Property {propertyName} does not exist in type {typeof(T).GetType().FullName}");
            return q.GroupBy(ex);
        }

        public IQueryable<IGrouping<TRet, T>> GroupWithObject<TRet>(IQueryable<T> q, Dictionary<string, string> dic) where TRet : class
        {
            var ex = Expressions.ObjectMapping<T, TRet>(dic);
            return q.GroupBy(ex);
        }

        public Expression<Func<T, bool>> GetExpression(PropertyFilter f)
        {
            switch (f.FilterType)
            {
                case "equals":
                    return Expressions.GetEqualsExpression<T>(f.MemberName, f.Value1);
                case "string":
                    return Expressions.GetStringContainsFilter<T>(f.MemberName, f.Value1);
                case "decimal":
                    return Expressions.GetRangeFilter<T>(f.MemberName, decimal.Parse(f.Value1), decimal.Parse(f.Value2));

                case "int":
                    return Expressions.GetRangeFilter<T>(f.MemberName, int.Parse(f.Value1), int.Parse(f.Value2));

                case "date":
                    var v1 = DateTime.MinValue;
                    var v2 = DateTime.MaxValue;

                    if (DateTime.TryParse(f.Value1, out DateTime dt))
                        v1 = dt.GetDayStart();

                    if (DateTime.TryParse(f.Value2, out DateTime dt2))
                        v2 = dt2.GetDayEnd();

                    return Expressions.GetRangeFilter<T>(f.MemberName, v1, v2);

                case "day":
                    var vd1 = DateTime.MinValue;
                    var vd2 = DateTime.MaxValue;

                    if (DateTime.TryParse(f.Value1, out DateTime dt3))
                    {
                        vd1 = dt3.GetDayStart();
                        vd2 = dt3.GetDayEnd();
                        return Expressions.GetRangeFilter<T>(f.MemberName, vd1, vd2);
                    }
                    break;
                case "reference":
                    return Expressions.GetReferenceContainedFilter<T>(f.MemberName, f.Ids);
            }
            return null;
        }

        
        public List<Expression> ToFilterExpressions(IEnumerable<PropertyFilter> fs)
        {
            List<Expression> exs = new List<Expression>();
            foreach (PropertyFilter f in fs)
            {
                var ex = GetExpression(f);
                if (ex != null)
                    exs.Add(ex);
            }

            return exs;
        }

        public List<Expression<Func<T, bool>>> ToStrongExpressions(IEnumerable<PropertyFilter> fs)
        {
            List<Expression<Func<T, bool>>> exs = new List<Expression<Func<T, bool>>>();
            foreach (PropertyFilter f in fs)
            {
                var ex = GetExpression(f);
                if (ex != null)
                    exs.Add(ex);
            }

            return exs;
        }

        public ListOptions<T> ToModelGetOptions(LoadOptions posted, bool ignoreFilters = false)
        {
            ListOptions<T> opts = new ListOptions<T>();
            opts.Skip = posted.Skip;
            opts.Showing = posted.Showing;
            opts.SearchTerm = posted.SearchTerm;
            SortDir dir;
            if (Enum.TryParse(posted.Direction, out dir))
                opts.Direction = dir;

            opts.OrderProperty = posted.OrderProperty;

            if (posted.Filters != null && !ignoreFilters)
            {
                opts.Filters = ToFilterExpressions(posted.PropertyFilters);
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
