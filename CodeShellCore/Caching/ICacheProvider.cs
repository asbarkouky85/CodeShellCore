using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using CodeShellCore.Services;

namespace CodeShellCore.Caching
{
    public interface ICacheProvider : IServiceBase
    {
        T Get<T>(object key) where T : class;
        List<T> GetAll<T>() where T : class;

        void Store<T>(object key, T entity) where T : class;

        bool Remove<T>(object key) where T : class;
        void RemoveAll<T>() where T : class;


    }
}
