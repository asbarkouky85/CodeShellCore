using CodeShellCore.Data;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    public interface ILocalizedRepository<T> : IRepository<T> where T : class
    {
        LoadResult LoadLocalized(LoadOptions opts);
   
    }
}
