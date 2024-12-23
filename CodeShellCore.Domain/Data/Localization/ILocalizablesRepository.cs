using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Localization
{
    public interface ILocalizablesRepository<T> : IRepository<T> where T : class, ILocalizable
    {
        Task Apply(string type, object id, int langId, IEnumerable<T> data);
        Task<IEnumerable<LocalizablesLoader>> Get(string type, object id, IEnumerable<int> langs);
    }
}
