using CodeShellCore.Data.MemberReplacement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Mapping
{
    public interface IQueryProjector
    {
        IQueryable<TDestination> Project<TSource, TDestination>(IQueryable<TSource> query);
        IQueryable<TEntity> ReplaceMembers<TEntity>(IQueryable<TEntity> query, Action<MemberReplacementConfiguration<TEntity>> conf);
    }
}
