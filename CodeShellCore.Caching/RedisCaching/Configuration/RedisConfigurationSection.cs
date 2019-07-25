namespace CodeShellCore.Caching.RedisCaching.Configuration
{
    public class RedisConfigurationSection 
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public long DatabaseId { get; set; }
    }
}
