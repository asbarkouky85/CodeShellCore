using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionConfigService
    {
        Expression<Func<T, bool>> GetCollectionExpression<T>(string id, IUserAccessor acc);
        void RegisterCollection<T>(string id, Func<IUserAccessor, Expression<Func<T, bool>>> exp);
    }
}
