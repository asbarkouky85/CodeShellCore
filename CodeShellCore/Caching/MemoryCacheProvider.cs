using System;
using System.Collections.Generic;
using System.Linq;

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

        public T Get<T>(object key) where T : class
        {
            if (getList<T>().TryGetValue(key.ToString(), out object ob))
                return (T)ob;

            return null;
        }
        public void Store<T>(object key, T entity) where T : class
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
        }

        public bool Remove<T>(object key) where T : class
        {
            if (Data.TryGetValue(typeof(T), out SortedList<string, object> lst))
            {
                lst.Remove(key.ToString());
                return true;
            }
            return false;
        }

        public List<T> GetAll<T>() where T : class
        {
            return getList<T>().Select(d => (T)d.Value).ToList();
        }

        public void RemoveAll<T>() where T : class
        {
            Data.Remove(typeof(T));
        }
    }
}
