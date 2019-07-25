using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Caching.RedisCaching.Configuration;
using CodeShellCore.Services;
using ServiceStack.Redis;

namespace CodeShellCore.Caching.RedisCaching.RedisCachingService
{

    public class RedisCacheService : ServiceBase, IRedisCacheService
    {
        private readonly RedisEndpoint _endPoint;
        private RedisConfigurationSection _config;
        public RedisCacheService()
        {
            _config = new RedisConfigurationSection
            {
                Host = Shell.GetConfigAs<string>("RedisConfig:Host"),
                Port = Shell.GetConfigAs<int>("RedisConfig:Port"),
                DatabaseId = Shell.GetConfigAs<int>("RedisConfig:DatabaseId")
            };

            _endPoint = new RedisEndpoint(_config.Host, _config.Port, null, _config.DatabaseId);
        }
        public void Set<T>(string key, T value)
        {
            Set(key, value, TimeSpan.Zero);
        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.As<T>().SetValue(key, value, timeout);
            }
        }

        public T Get<T>(string key)
        {
            T result = default(T);

            using (RedisClient client = new RedisClient(_endPoint))
            {
                var wrapper = client.As<T>();

                result = wrapper.GetValue(key);
            }

            return result;
        }


        public T GetById<T>(int id)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {

                var entity = client.GetById<T>(id);
                return entity;
            }
        }

        public IList<T> FindBy<T>(Func<T, bool> predicate) where T : class
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                return client.GetAll<T>().Where(predicate).ToList();
            }
        }

        public T Store<T>(T entity)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.Store(entity);
            }

            return entity;
        }

        public void StoreAll<T>(IEnumerable<T> entities)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.StoreAll(entities);
            }
        }
        public IList<T> GetAll<T>()
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                var result = client.GetAll<T>().ToList();
                return result;
            }
        }

        public IList<T> GetByIds<T>(ICollection Ids)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                var result = client.GetByIds<T>(Ids);
                return result;
            }
        }

        public IList<T> GetByIdsList<T>(List<string> ids)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                List<string> keys = ids.ConvertAll(i => i.ToString());
                var result = client.As<IEnumerable<T>>().GetValues(keys).SelectMany(d => d);
                return result.ToList();
            }
        }

        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }
    }


}
