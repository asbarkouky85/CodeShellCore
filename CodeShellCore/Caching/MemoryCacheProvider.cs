using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Caching
{
    public class MemoryCacheProvider : ICacheProvider
    {
        static Dictionary<Type, SortedList<string, object>> Data = new Dictionary<Type, SortedList<string, object>>();
        public void Dispose() { }

        private SortedList<string, object> getList<T>()
        {

            if (Data.TryGetValue(typeof(T), out SortedList<string, object> lst))
            {
                return lst;
            }
            else
            {
                return new SortedList<string, object>();
            }
        }

        public Task<T> Get<T>(string key) where T : class
        {
            return Task.Run(() =>
            {
                if (key != null && getList<T>().TryGetValue(key, out object ob))
                    return (T)ob;

                return null;
            });
        }
        public Task Store<T>(string key, T entity) where T : class
        {
            return Task.Run(() =>
            {
                if (Data.TryGetValue(typeof(T), out SortedList<string, object> lst))
                {
                    lst[key.ToString()] = entity;
                }
                else
                {
                    Data[typeof(T)] = new SortedList<string, object>();
                    Data[typeof(T)][key.ToString()] = entity;
                }
            });
        }

        public Task<bool> Remove<T>(string key) where T : class
        {
            return Task.Run(() =>
            {
                if (Data.TryGetValue(typeof(T), out SortedList<string, object> lst))
                {
                    lst.Remove(key.ToString());
                    return true;
                }
                return false;
            });
        }

        public Task<List<T>> GetAll<T>() where T : class
        {
            return Task.Run(() =>
            {

                return getList<T>().Select(d => (T)d.Value).ToList();
            });
        }

        public Task RemoveAll<T>() where T : class
        {
            return Task.Run(() =>
            {
                Data.Remove(typeof(T));
            });
        }
    }
}
