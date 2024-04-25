using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.MQ;

namespace CodeShellCore.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<TR> MapList<T, TR>(this List<T> lst) where T : class where TR : class
        {
            Expression<Func<T, TR>> expression = ExpressionStore.GetExpression<T, TR>();
            return lst.Select(expression.Compile()).ToList();
        }

        public static IEnumerable<TR> MapList<T, TR>(this IQueryable<T> lst) where T : class where TR : class
        {
            return lst.Select(ExpressionStore.GetExpression<T, TR>()).ToList();
        }

        public static IEnumerable<TR> MapList<T, TR>(this IEnumerable<T> lst) where T : class where TR : class
        {
            Expression<Func<T, TR>> expression = ExpressionStore.GetExpression<T, TR>();
            if (lst is IQueryable<T>)
                ((IQueryable<T>)lst).Select(expression).ToList();
            return lst.Select(expression.Compile()).ToList();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> lst, Action<T> func)
        {
            foreach (T item in lst)
                func(item);
            return lst;
        }

        

        public static List<T> MapTo<T>(this IEnumerable lst, bool ignoreId = true, IEnumerable<string> ignore = null) where T : class
        {
            List<T> t = new List<T>();
            foreach (var ob in lst)
            {
                t.Add(ob.MapTo<T>(ignoreId, ignore));
            }
            return t;
        }

        public static T MapTo<T>(this object obj, bool ignoreId = true, IEnumerable<string> ignore = null) where T : class
        {
            T inst = Activator.CreateInstance<T>();
            inst.AppendProperties(obj, ignoreId, ignore);
            return inst;
        }

        public static object Copy(this ISharedModel obj)
        {
            object ob = Activator.CreateInstance(obj.GetType());
            ob.AppendProperties(obj, false, obj.GetNavPropertyNames());
            return ob;
        }

        public static void AppendProperties(this object model, object ob, bool ignoreId = true, IEnumerable<string> ignore = null)
        {
            ignore = ignore == null ? new List<string>() : ignore;
            PropertyInfo[] props = ob.GetType().GetProperties();
            Dictionary<string, PropertyInfo> modelProps = model.GetType()
                .GetProperties()
                .Where(
                    d => (d.Name != "Id" || !ignoreId) &&
                    (d.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(d.PropertyType)) &&
                    !ignore.Contains(d.Name) &&
                    d.CanWrite
                )
                .ToDictionary(d => d.Name);

            foreach (PropertyInfo inf in props)
            {
                if (modelProps.ContainsKey(inf.Name))
                {
                    object v = inf.GetValue(ob);
                    modelProps[inf.Name].SetValue(model, v);
                }
            }
        }
        public static Expression<Func<T, bool>> Combine<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> exp2)
        {
            ParameterExpression para = Expression.Parameter(typeof(T));
            BinaryExpression combinedExpression = Expression.MakeBinary(ExpressionType.And, exp, exp2);
            return Expression.Lambda<Func<T, bool>>(combinedExpression, para);
        }

        

        
    }
}
