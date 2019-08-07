using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Caching
{
    public class MemoryCacheProvider : ICacheProvider
    {
        static Dictionary<Type, SortedList<object, object>> Data = new Dictionary<Type, SortedList<object, object>>();
        public void Dispose() { }

        private SortedList<object, object> getList<T>()
        {
            if (Data.TryGetValue(typeof(T), out SortedList<object, object> lst))
            {
                return lst;
            }
            else
            {
                return new SortedList<object, object>();
            }
        }

        public T Get<T>(object key) where T : class
        {
            if (getList<T>().TryGetValue(key, out object ob))
                return (T)ob;

            return null;
        }
        public void Store<T>(object key, T entity) where T : class
        {
            if (Data.TryGetValue(typeof(T), out SortedList<object, object> lst))
            {
                lst[key] = entity;
            }
            else
            {
                Data[typeof(T)] = new SortedList<object, object>();
                Data[typeof(T)][key] = entity;
            }
        }

        public bool Remove<T>(object key) where T : class
        {
            if (Data.TryGetValue(typeof(T), out SortedList<object, object> lst))
            {
                lst.Remove(key);
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
