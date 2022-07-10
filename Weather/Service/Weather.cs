using System.Text.Json;
using DataTemplates;
using Microsoft.Extensions.Caching.Distributed;
using Weather.Interface;

namespace Weather.Service;

public class Weather : IWeather
{
    private readonly IDistributedCache _cache;
    
    public Weather(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<RedisTemplate?> GetWeatherAsync(string city)
    {
        var key = $"{DateTime.Now.AddHours(1):ddMMyyhh}:{city}";
        var data = await _cache.GetStringAsync(key);
        
        if (data == null)
        {
            return null;
        }

        return JsonSerializer.Deserialize<RedisTemplate>(data);
    }
}