using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    public interface ILocalizablesRepository<T> : IRepository<T> where T : class, ILocalizable
    {
        void Apply(string type, long id, int langId, IEnumerable<T> data);

        IEnumerable<LocalizablesLoader> Get(string type, long id, IEnumerable<int> langs);

    }
}
