using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Localization
{
    public interface ILocalizablesRepository<T> : IRepository<T> where T : class, ILocalizable
    {
        void Apply(string type, object id, int langId, IEnumerable<T> data);

        IEnumerable<LocalizablesLoader> Get(string type, object id, IEnumerable<int> langs);

    }
}
