using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Helpers;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.Text;
using CodeShellCore.Types;

namespace CodeShellCore.Linq
{
    public class ExpressionGenerator<T> : IExpressionGenerator<T> where T : class
    {


        public Expression<Func<T, bool>> GetRangeFilter(string propertyName, int start, int end)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);
            BinaryExpression greaterExp = null;
            BinaryExpression smaller = null;

            if (!start.Equals(0))
            {
                ConstantExpression sExp = Expression.Constant(start);
                greaterExp = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, mExp, sExp);
            }

            if (!end.Equals(0))
            {
                ConstantExpression eExp = Expression.Constant(end);
                smaller = Expression.MakeBinary(ExpressionType.LessThanOrEqual, mExp, eExp);
            }

            return Combine(pExp, greaterExp, smaller);
        }

        public Expression<Func<T, bool>> GetRangeFilter(string propertyName, DateTime start, DateTime end)
        {

            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);

            BinaryExpression greaterExp = null;
            BinaryExpression smaller = null;

            UnaryExpression uExp = Expression.Convert(mExp, typeof(DateTime));

            if (start != DateTime.MinValue)
            {
                ConstantExpression sExp = Expression.Constant(start, typeof(DateTime));
                greaterExp = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, uExp, sExp);
            }

            if (end != DateTime.MaxValue)
            {
                ConstantExpression eExp = Expression.Constant(end, typeof(DateTime));
                smaller = Expression.MakeBinary(ExpressionType.LessThanOrEqual, uExp, eExp);
            }

            return Combine(pExp, greaterExp, smaller);
        }

        public IQueryable<T> SortWith(IQueryable<T> q, string propertyName, SortDir dir)
        {
            PropertyInfo prop = typeof(T).GetProperty(propertyName);

            if (prop == null)
                throw new Exception("no such property " + propertyName + " in class " + typeof(T).Name);

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
            return q.GroupBy(ex);
        }

        public IQueryable<IGrouping<TRet, T>> GroupWithObject<TRet>(IQueryable<T> q, Dictionary<string, string> dic) where TRet : class
        {
            var ex = Expressions.ObjectMapping<T, TRet>(dic);
            return q.GroupBy(ex);
        }

        //public IQueryable<IGrouping<dynamic, T>> GroupWithDynamic<TRet>(IQueryable<T> q, Dictionary<string, string> dic) where TRet : class
        //{
        //    var ex = Expressions.ObjectMapping<T, TRet>(dic);
        //    return q.GroupBy(ex);
        //}

        public Expression<Func<T, bool>> GetRangeFilter(string propertyName, decimal start, decimal end)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);
            BinaryExpression greaterExp = null;
            BinaryExpression smaller = null;

            if (!start.Equals(0))
            {
                ConstantExpression sExp = Expression.Constant(start);
                greaterExp = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, mExp, sExp);
            }

            if (!end.Equals(0))
            {
                ConstantExpression eExp = Expression.Constant(end);
                smaller = Expression.MakeBinary(ExpressionType.LessThanOrEqual, mExp, eExp);
            }

            return Combine(pExp, greaterExp, smaller);
        }

        Expression<Func<T, bool>> Combine(ParameterExpression pExp, BinaryExpression greaterExp, BinaryExpression smaller)
        {
            if (greaterExp == null && smaller != null)
            {
                return Expression.Lambda<Func<T, bool>>(smaller, pExp);
            }
            else if (greaterExp != null && smaller == null)
            {
                return Expression.Lambda<Func<T, bool>>(greaterExp, pExp);
            }
            else if (greaterExp != null && smaller != null)
            {
                BinaryExpression combinedExpression = Expression.MakeBinary(ExpressionType.And, greaterExp, smaller);
                return Expression.Lambda<Func<T, bool>>(combinedExpression, pExp);
            }
            else
                return null;
        }

        public MemberExpression GetMemberExpression(ParameterExpression pExp, string propertyName)
        {
            MemberExpression mExp = null;
            if (propertyName.Contains("."))
            {
                string[] parts = propertyName.Split('.');
                mExp = Expression.Property(pExp, parts[0]);
                for (int i = 1; i < parts.Length; i++)
                {
                    mExp = Expressions.FromNavigation(mExp, parts[i]);
                }
            }
            else
            {
                mExp = Expression.Property(pExp, propertyName);
            }
            return mExp;
        }

        public Expression<Func<T, bool>> GetStringContainsFilter(string propertyName, string str)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);

            MethodInfo inf = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            ConstantExpression cExp = Expression.Constant(str, typeof(string));
            MethodCallExpression mcExp = Expression.Call(mExp, inf, cExp);

            return Expression.Lambda<Func<T, bool>>(mcExp, pExp);
        }

        public Expression<Func<T, bool>> GetReferenceContainedFilter(string propertyName, IEnumerable<string> ids)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);

            Type method;
            Type typ = null;
            IEnumerable lst;
            if (!mExp.Type.IsNullable())
            {
                method = typeof(IEnumerable<long>);
                lst = new List<long>();
                foreach (var i in ids)
                {
                    ((List<long>)lst).Add(long.Parse(i));
                }
                typ = typeof(long);
            }
            else
            {
                method = typeof(IEnumerable<long?>);
                lst = new List<long?>();
                foreach (var i in ids)
                {
                    long? rr=null;
                  
                    ((List<long?>)lst).Add(long.TryParse(i, out  long ss)?ss: rr);
                }
                typ = typeof(long?);
            }
            Expression conv = Expression.Convert(mExp, typ);

            ConstantExpression cExpr = Expression.Constant(lst);
            MethodCallExpression mExpr = Expression.Call(typeof(Enumerable), "Contains", new[] { typ }, cExpr, conv);

            return Expression.Lambda<Func<T, bool>>(mExpr, pExp);
        }

        public Expression<Func<T, bool>> GetExpression(PropertyFilter f)
        {
            switch (f.FilterType)
            {
                case "string":
                    return GetStringContainsFilter(f.MemberName, f.Value1);
                case "decimal":
                    return GetRangeFilter(f.MemberName, decimal.Parse(f.Value1), decimal.Parse(f.Value2));

                case "int":
                    return GetRangeFilter(f.MemberName, int.Parse(f.Value1), int.Parse(f.Value2));

                case "date":
                    var v1 = DateTime.MinValue;
                    var v2 = DateTime.MaxValue;

                    if (DateTime.TryParse(f.Value1, out DateTime dt))
                        v1 = dt.GetDayStart();

                    if (DateTime.TryParse(f.Value2, out DateTime dt2))
                        v2 = dt2.GetDayEnd();

                    return GetRangeFilter(f.MemberName, v1, v2);

                case "day":
                    var vd1 = DateTime.MinValue;
                    var vd2 = DateTime.MaxValue;

                    if (DateTime.TryParse(f.Value1, out DateTime dt3))
                    {
                        vd1 = dt3.GetDayStart();
                        vd2 = dt3.GetDayEnd();
                        return GetRangeFilter(f.MemberName, vd1, vd2);
                    }
                    break;
                case "reference":
                    return GetReferenceContainedFilter(f.MemberName, f.Ids);
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
