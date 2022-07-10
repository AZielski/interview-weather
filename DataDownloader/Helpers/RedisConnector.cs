using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace DataDownloader.Helpers;

public class RedisConnector : IDisposable
{
    private RedisCache Redis { get; set; }
    
    public void Dispose()
    {
        Redis.Dispose();
        GC.SuppressFinalize(this);
    }

    public RedisCache ConnectToRedis()
    {
        var address = Environment.GetEnvironmentVariable("REDIS_HOST");
        var port = Environment.GetEnvironmentVariable("REDIS_PORT");
        
        Redis = new RedisCache(new RedisCacheOptions()
        {
            Configuration = $"{address}:{port}",
        });
        
        return Redis;
    }
}