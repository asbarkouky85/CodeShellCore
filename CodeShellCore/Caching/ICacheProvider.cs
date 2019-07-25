using System;
using System.Collections;
using System.Collections.Generic;
using CodeShellCore.Services;

namespace CodeShellCore.Caching
{
    public interface ICacheProvider:IServiceBase
    {
        void Set<T>(string key, T value);
        /// <summary>
        /// Set Value Of Given Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeout"></param>
        void Set<T>(string key, T value, TimeSpan timeout);
        /// <summary>
        /// Get Value Of Given Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// Get Object By Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(int id);
        /// <summary>
        /// Find Object Depending On Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IList<T> FindBy<T>(Func<T, bool> predicate) where T : class;
        /// <summary>
        /// Store Whole Object To Cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Store<T>(T entity);
        /// <summary>
        /// Store List Of Objects To Cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void StoreAll<T>(IEnumerable<T> entities);
        /// <summary>
        /// Get All Objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetAll<T>();
        /// <summary>
        /// Get All Objects Depending On List Of Ids
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<T> GetByIds<T>(ICollection ids);

        /// <summary>
        /// Get Data By The Passed List Of Keys
        /// </summary>
        /// <param name="ids"></param>
        IList<T> GetByIdsList<T>(List<string> ids);

        /// <summary>
        /// remove key from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);
        /// <summary>
        /// check if key exist in Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsInCache(string key);
    }
}
