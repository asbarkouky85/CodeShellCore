using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionConfigService
    {
        Expression<Func<T, bool>> GetCollectionExpression<T>(string id);
        void RegisterCollection<T>(string id, Func<Expression<Func<T, bool>>> exp);
    }
}
