using CodeShellCore.Security;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public class CollectionConfigService : ServiceBase, ICollectionConfigService
    {
        private static Dictionary<Type, Dictionary<string, Func<IUserAccessor, Expression>>> registered = new Dictionary<Type, Dictionary<string, Func<IUserAccessor, Expression>>>();

        public Expression<Func<T, bool>> GetCollectionExpression<T>(string id, IUserAccessor acc)
        {
            var t = typeof(T);
            if (registered.TryGetValue(t, out Dictionary<string, Func<IUserAccessor, Expression>> stored))
            {
                if (stored.TryGetValue(id, out Func<IUserAccessor, Expression> expressionMaker))
                {
                    return (Expression<Func<T, bool>>)expressionMaker.Invoke(acc);
                }
            }
            return null;
        }


        public void RegisterCollection<T>(string id, Func<IUserAccessor, Expression<Func<T, bool>>> exp)
        {
            var t = typeof(T);
            if (registered.TryGetValue(t, out Dictionary<string, Func<IUserAccessor, Expression>> stored))
            {
                stored[id] = exp;
            }
            else
            {
                registered[t] = new Dictionary<string, Func<IUserAccessor, Expression>>();
                registered[t][id] = exp;
            }
        }
    }
}
