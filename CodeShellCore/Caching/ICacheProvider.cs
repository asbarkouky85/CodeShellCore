using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using CodeShellCore.Services;

namespace CodeShellCore.Caching
{
    public interface ICacheProvider : IServiceBase
    {
        T Get<T>(string key) where T : class;
        List<T> GetAll<T>() where T : class;

        void Store<T>(string key, T entity) where T : class;

        bool Remove<T>(string key) where T : class;
        void RemoveAll<T>() where T : class;


    }
}
