using CodeShellCore.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Linq
{
    public class ExpressionStore
    {
        static Dictionary<Type, Dictionary<Type, Expression>> dictionary = new Dictionary<Type, Dictionary<Type, Expression>>();
        static Dictionary<Type, Func<string, Expression>> searches = new Dictionary<Type, Func<string, Expression>>();
        public static void Register<T, TVM>(Expression<Func<T, TVM>> expression) where T : class where TVM : class
        {
            if (!dictionary.ContainsKey(typeof(T)))
            {
                dictionary[typeof(T)] = new Dictionary<Type, Expression>();
            }
            dictionary[typeof(T)][typeof(TVM)] = expression;
        }

        public static void RegisterSearchExpression<T>(Func<string, Expression<Func<T, bool>>> predicate)
        {
            searches[typeof(T)] = predicate;
        }

        static ExpressionStore()
        {
            RegisterSearchExpression<Named<long>>(term => { return e => e.Name.Contains(term); });
            RegisterSearchExpression<Described<long>>(term => { return e => e.Name.Contains(term) || e.Description.Contains(term); });
        }

        public static Expression<Func<T, bool>> GetSearchExpression<T>(string str)
        {
            if (searches.TryGetValue(typeof(T), out Func<string, Expression> exp))
            {
                Func<string, Expression<Func<T, bool>>> func = (Func<string, Expression<Func<T, bool>>>)exp;
                return func.Invoke(str);
            }
            return null;
        }

        public static Expression<Func<T, TVM>> GetExpression<T, TVM>() where T : class where TVM : class
        {
            if (!dictionary.ContainsKey(typeof(T)))
                throw new Exception("Unregistered mapping between " + typeof(T).Name + " and " + typeof(TVM));

            Dictionary<Type, Expression> dic = dictionary[typeof(T)];
            if (!dic.ContainsKey(typeof(TVM)))
                throw new Exception("Unregistered mapping between " + typeof(T).Name + " and " + typeof(TVM));

            return (Expression<Func<T, TVM>>)dic[typeof(TVM)];
        }
    }
}
