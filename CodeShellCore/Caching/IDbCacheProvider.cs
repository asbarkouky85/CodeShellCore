using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Caching
{
    public interface IDbCacheProvider
    {
        T GetById<T>(object id) where T : class;
        void Store<T>(T entity) where T : class;
        List<T> GetAllIds<T>(ICollection ids) where T : class;
        void RemoveById<T>(object id) where T : class;
        bool KeyExists(string key);
    }
}
