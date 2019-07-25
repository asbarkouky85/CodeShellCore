using CodeShellCore.Data;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    public interface ILocalizedDescribedRepository<T>  where T : class, IDescribedModel
    {
        LoadResult<Described<long>> FindAsDescribedLocalized(LoadOptions opts, Expression<Func<T, bool>> filter = null);
    }
}
