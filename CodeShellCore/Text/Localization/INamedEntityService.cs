using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    public interface INamedEntityService<T> : IEntityService<T> where T : class, INamed<long>
    {
        LoadResult<Named<long>> LoadNamed(LoadOptions opts);
    }
}
