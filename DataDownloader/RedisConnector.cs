using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace DataDownloader;

public class RedisConnector : IDisposable
{
    private RedisCache Redis { get; set; }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public RedisCache ConnectToRedis()
    {
        var address = Environment.GetEnvironmentVariable("REDIS_HOST");
        var port = Environment.GetEnvironmentVariable("REDIS_PORT");
        
        var cache = new RedisCache(new RedisCacheOptions()
        {
            Configuration = $"{address}:{port}",
        });
        
        Redis = cache;
        return cache;
    }
}