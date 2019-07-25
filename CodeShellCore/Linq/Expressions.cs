using CodeShellCore.Data;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeShellCore.Linq
{
    public static class Expressions
    {
        public static MemberExpression FromNavigation(Expression ex, string property)
        {
            return Expression.Property(ex, property);
        }

        public static IEnumerable<Expression<Func<T, bool>>> GetFilters<T>(IEnumerable<PropertyFilter> filters) where T : class
        {
            return new ExpressionGenerator<T>().ToStrongExpressions(filters);
        }

        public static Expression<Func<T, object>> Property<T>(string property) where T : class
        {
            var par = Expression.Parameter(typeof(T));
            var prop = Expression.Property(par, property);

            return Expression.Lambda<Func<T, object>>(prop, par);
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
                mExp = Expression.Property(pExp, parts[0]);
                for (int i = 1; i < parts.Length; i++)
                {
                    mExp = FromNavigation(mExp, parts[i]);
                }
            }
            else
            {
                mExp = Expression.Property(pExp, propertyName);
            }
            return mExp;
        }

        public static Expression<Func<T, TVal>> PropertyExpression<T, TVal>(string property) where T : class
        {
            var par = Expression.Parameter(typeof(T));
            var prop = GetMemberExpression(par, property);
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

        public static Expression<Func<T, bool>> StringContains<T>(string property, string value) where T : IModel
        {
            return StringMethod<T>("Contains", property, value);
        }

        public static Expression<Func<T, bool>> StringMethod<T>(string methodName, string property, string value) where T : IModel
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
