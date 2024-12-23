using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeShellCore.Services;

namespace CodeShellCore.Caching
{
    public interface ICacheProvider : IServiceBase
    {
        Task<T> Get<T>(string key) where T : class;
        Task<List<T>> GetAll<T>() where T : class;

        Task Store<T>(string key, T entity) where T : class;

        Task<bool> Remove<T>(string key) where T : class;
        Task RemoveAll<T>() where T : class;


    }
}
