

namespace CodeShellCore.Caching.RedisCaching.Configuration
{
    public static  class RedisConfigurationManager
    {
        private const string SectionName = "RedisConfig";

        public static RedisConfigurationSection Config => Shell.GetConfigAs<RedisConfigurationSection>(SectionName);
    }
}
