using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Caching
{
    public interface IDbCacheProvider
    {
        Task<T> GetById<T>(string id) where T : class;
        Task Store<T>(T entity) where T : class;
        Task<List<T>> GetAllIds<T>(ICollection ids) where T : class;
        Task RemoveById<T>(object id) where T : class;
        Task<bool> KeyExists(string key);
    }
}
