using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Data.Mapping
{
    public interface IQueryProjector
    {
        IQueryable<TDestination> Project<TSource, TDestination>(IQueryable<TSource> query);
    }
}
