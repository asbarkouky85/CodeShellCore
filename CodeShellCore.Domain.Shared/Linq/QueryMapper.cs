using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Linq
{
    public class QueryMapper
    {
        static Dictionary<Type, Dictionary<Type, Expression>> dictionary = new Dictionary<Type, Dictionary<Type, Expression>>();
        public static void Register<T, TVM>(Expression<Func<T, TVM>> expression) where T : class where TVM : class
        {
            if (!dictionary.ContainsKey(typeof(T)))
            {
                dictionary[typeof(T)] = new Dictionary<Type, Expression>();
            }
            dictionary[typeof(T)][typeof(TVM)] = expression;
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
