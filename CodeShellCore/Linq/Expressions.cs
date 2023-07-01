using CodeShellCore.Helpers;
using CodeShellCore.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CodeShellCore.Linq
{
    public static class Expressions
    {
        public static MemberExpression GetPropertyExpressionFromExpression(Expression ex, string property)
        {
            if (ex.Type.GetProperties().Any(d => d.Name == property))
                return Expression.Property(ex, property);
            return null;
        }

        

        public static Expression<Func<T, object>> Property<T>(string property) where T : class
        {
            var par = Expression.Parameter(typeof(T));
            var prop = Expression.Property(par, property);

            return Expression.Lambda<Func<T, object>>(prop, par);
        }

        public static Expression<Func<T, bool>> Combine<T>(ParameterExpression pExp, BinaryExpression greaterExp, BinaryExpression smaller)
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

        public static Expression<Func<T, bool>> GetRangeFilter<T>(string propertyName, DateTime start, DateTime end)
        {

            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);

            if (mExp == null)
                return null;

            BinaryExpression greaterExp = null;
            BinaryExpression smaller = null;

            UnaryExpression uExp = Expression.Convert(mExp, typeof(DateTime));

            if (start != DateTime.MinValue)
            {
                start = start.RemoveMilli();
                ConstantExpression sExp = Expression.Constant(start, typeof(DateTime));
                greaterExp = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, uExp, sExp);
            }

            if (end != DateTime.MaxValue)
            {
                end = end.RemoveMilli();
                ConstantExpression eExp = Expression.Constant(end, typeof(DateTime));
                smaller = Expression.MakeBinary(ExpressionType.LessThanOrEqual, uExp, eExp);
            }

            return Combine<T>(pExp, greaterExp, smaller);
        }

        public static Expression<Func<T, bool>> GetRangeFilter<T>(string propertyName, int start, int end)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);
            if (mExp == null)
                return null;
            BinaryExpression greaterExp = null;
            BinaryExpression smaller = null;

            UnaryExpression uExp = Expression.Convert(mExp, typeof(int));

            if (!start.Equals(0))
            {
                ConstantExpression sExp = Expression.Constant(start);
                greaterExp = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, uExp, sExp);
            }

            if (!end.Equals(0))
            {
                ConstantExpression eExp = Expression.Constant(end);
                smaller = Expression.MakeBinary(ExpressionType.LessThanOrEqual, uExp, eExp);
            }

            return Combine<T>(pExp, greaterExp, smaller);
        }

        static object Parse(string t, Type typ)
        {
            var nam = typ.RealType().Name;
            switch (nam)
            {
                case "Int32":
                case "Int64":
                case "Int16":
                    return int.Parse(t);
                case "Boolean":
                    return bool.Parse(t);
            }
            return t;
        }

        public static Expression<Func<T, bool>> GetEqualsExpression<T>(string propertyName, string value1)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);
            if (mExp == null)
                return null;
            var real = mExp.Type.RealType();
            MethodInfo inf = real.GetMethod("Equals", new[] { real.RealType() });
            var ob = Parse(value1, mExp.Type);
            ConstantExpression cExp = Expression.Constant(ob);
            UnaryExpression uExp = Expression.Convert(cExp, real);
            MethodCallExpression mcExp = Expression.Call(mExp, inf, uExp);

            return Expression.Lambda<Func<T, bool>>(mcExp, pExp);
        }

        public static Expression<Func<T, bool>> GetStringContainsFilter<T>(string propertyName, string str)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);
            if (mExp == null)
                return null;
            MethodInfo inf = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            ConstantExpression cExp = Expression.Constant(str, typeof(string));
            MethodCallExpression mcExp = Expression.Call(mExp, inf, cExp);

            return Expression.Lambda<Func<T, bool>>(mcExp, pExp);
        }

        public static Expression<Func<T, bool>> GetReferenceContainedFilter<T>(string propertyName, IEnumerable<string> ids)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);
            if (mExp == null)
                return null;
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
                    long? rr = null;

                    ((List<long?>)lst).Add(long.TryParse(i, out long ss) ? ss : rr);
                }
                typ = typeof(long?);
            }
            Expression conv = Expression.Convert(mExp, typ);

            ConstantExpression cExpr = Expression.Constant(lst);
            MethodCallExpression mExpr = Expression.Call(typeof(Enumerable), "Contains", new[] { typ }, cExpr, conv);

            return Expression.Lambda<Func<T, bool>>(mExpr, pExp);
        }

        public static Expression<Func<T, bool>> GetRangeFilter<T>(string propertyName, decimal start, decimal end)
        {
            ParameterExpression pExp = Expression.Parameter(typeof(T));
            MemberExpression mExp = GetMemberExpression(pExp, propertyName);
            if (mExp == null)
                return null;
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

            return Combine<T>(pExp, greaterExp, smaller);
        }

        public static Expression<Func<T, bool>> Filter<T>(Expression<Func<T, bool>> f)
        {
            return f;
        }

        public static MemberExpression GetMemberExpression(ParameterExpression pExp, string propertyName)
        {
            MemberExpression mExp = null;
            if (propertyName.Contains("."))
            {
                string[] parts = propertyName.Split('.');
                if (pExp.Type.GetProperties().Any(d => d.Name == propertyName))
                {
                    mExp = Expression.Property(pExp, parts[0]);
                    for (int i = 1; i < parts.Length; i++)
                    {
                        mExp = GetPropertyExpressionFromExpression(mExp, parts[i]);
                    }
                }

            }
            else
            {
                if (pExp.Type.GetProperties().Any(d => d.Name == propertyName))
                    mExp = Expression.Property(pExp, propertyName);
            }
            return mExp;
        }

        public static Expression<Func<T, TVal>> PropertyExpression<T, TVal>(string property) where T : class
        {
            var par = Expression.Parameter(typeof(T));
            var prop = GetMemberExpression(par, property);
            if (prop == null)
                return null;
            var propcon = Expression.Convert(prop, typeof(TVal));
            return Expression.Lambda<Func<T, TVal>>(propcon, par);
        }

        public static Expression<Func<T, TVal>> ObjectMapping<T, TVal>(Dictionary<string, string> mapping) where T : class where TVal : class
        {
            var inp = Expression.Parameter(typeof(T));
            var creat = Expression.New(typeof(TVal).GetConstructors().FirstOrDefault());
            var lst = new List<MemberBinding>();
            foreach (var map in mapping)
            {

                var mem = GetMemberExpression(inp, map.Value);
                var bind = Expression.Bind(typeof(TVal).GetMember(map.Key).First(), mem);
                lst.Add(bind);
            }

            var ass = Expression.MemberInit(creat, lst);
            return Expression.Lambda<Func<T, TVal>>(ass, inp);
        }

        public static Expression<Func<T, bool>> StringContains<T>(string property, string value)
        {
            return StringMethod<T>("Contains", property, value);
        }

        public static Expression<Func<T, bool>> StringMethod<T>(string methodName, string property, string value)
        {
            var method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
            if (method != null)
            {
                ParameterExpression paramExpr = Expression.Parameter(typeof(T));
                MemberExpression memExpr = Expression.Property(paramExpr, property);
                ConstantExpression cExpr = Expression.Constant(value, typeof(string));
                MethodCallExpression mExpr = Expression.Call(memExpr, method, cExpr);

                return Expression.Lambda<Func<T, bool>>(mExpr, paramExpr);
            }
            return x => true;
        }

        public static Expression<Func<T, bool>> Unique<T, TPrimary>(TPrimary id, string property, object value, string idProperty = "Id") where T : class
        {
            PropertyInfo pi = typeof(T).GetProperty(property);

            ParameterExpression paramExpr = Expression.Parameter(typeof(T));
            MemberExpression idPropertyExp = Expression.Property(paramExpr, idProperty);
            MemberExpression comparePropertyExp = Expression.Property(paramExpr, property);

            ConstantExpression idValueExp = Expression.Constant(id, typeof(TPrimary));
            ConstantExpression rawValueExp = Expression.Constant(value);
            UnaryExpression compareValueExp = Expression.Convert(rawValueExp, pi.PropertyType);

            BinaryExpression ex = Expression.MakeBinary(ExpressionType.Equal, comparePropertyExp, compareValueExp);
            BinaryExpression ex2 = Expression.MakeBinary(ExpressionType.NotEqual, idPropertyExp, idValueExp);
            BinaryExpression ex3 = Expression.MakeBinary(ExpressionType.And, ex, ex2);

            return Expression.Lambda<Func<T, bool>>(ex3, paramExpr);
        }
    }
}
