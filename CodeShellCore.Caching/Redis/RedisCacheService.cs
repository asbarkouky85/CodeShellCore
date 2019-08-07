using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Services;
using ServiceStack.Redis;

namespace CodeShellCore.Caching.Redis
{

    public class RedisCacheService : ServiceBase, ICacheProvider, IDbCacheProvider
    {
        private readonly RedisEndpoint _endPoint;
        private RedisConfig _config;
        public RedisCacheService()
        {
            _config = new RedisConfig
            {
                Host = Shell.GetConfigAs<string>("RedisConfig:Host"),
                Port = Shell.GetConfigAs<int>("RedisConfig:Port"),
                DatabaseId = Shell.GetConfigAs<int>("RedisConfig:DatabaseId")
            };

            _endPoint = new RedisEndpoint(_config.Host, _config.Port, null, _config.DatabaseId);
        }

        public T Get<T>(object key) where T : class
        {
            
            using (RedisClient client = new RedisClient(_endPoint))
            {
                return client.Get<T>(typeof(T).Name + ":" + key);
            }
        }


        public T GetById<T>(object id) where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                var entity = client.GetById<T>(id);
                return entity;
            }
        }

        public void Store<T>(object key, T entity) where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.Set(typeof(T).Name + ":" + key, entity);
            }
        }

        public void Store<T>(T entity) where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.Store(entity);
            }
        }

        public void StoreList<T>(IEnumerable<T> entities) where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.StoreAll(entities);
            }
        }
        public List<T> GetAll<T>() where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                return client.GetAll<T>().ToList();
            }
        }

        public List<T> GetAllIds<T>(ICollection Ids) where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                return client.GetByIds<T>(Ids).ToList();
            }
        }

        public bool Remove<T>(object key) where T : class
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(typeof(T) + ":" + key);
            }

            return removed;
        }

        public bool KeyExists(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }

        public void RemoveAll<T>() where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.DeleteAll<T>();
            }
        }

        public void RemoveById<T>(object id) where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.DeleteById<T>(id);
            }
        }
    }


}
