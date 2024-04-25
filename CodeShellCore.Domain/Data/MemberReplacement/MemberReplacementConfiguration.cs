using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.MemberReplacement
{
    public class MemberReplacementConfiguration<TEntity>
    {
        List<Tuple<Expression<Func<TEntity, object>>, Expression<Func<TEntity, object>>>> mappers = new List<Tuple<Expression<Func<TEntity, object>>, Expression<Func<TEntity, object>>>>();
        public MemberReplacementConfiguration<TEntity> AddReplacement(Expression<Func<TEntity, object>> member, Expression<Func<TEntity, object>> replacement)
        {
            mappers.Add(new Tuple<Expression<Func<TEntity, object>>, Expression<Func<TEntity, object>>>(member, replacement));
            return this;
        }

       public Tuple<Expression<Func<TEntity, object>>, Expression<Func<TEntity, object>>>[] GetReplaces()
        {
            return mappers.ToArray();
        }
    }
}
