using System.Text.Json;
using DataTemplates;
using Microsoft.Extensions.Caching.Distributed;
using Weather.Interfaces;

namespace Weather.Services;

public class WeatherService : IWeatherService
{
    private readonly IDistributedCache _cache;
    
    public WeatherService(IDistributedCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Gets weather data for a given city.
    /// </summary>
    /// <param name="city">City name</param>
    /// <returns>Weather data</returns>
    public async Task<RedisTemplate?> GetWeatherByCityAsync(string city)
    {
        var key = $"{DateTime.Now.AddHours(1):ddMMyyhh}:{city}";
        var data = await _cache.GetStringAsync(key);
        
        return data == null ? null : JsonSerializer.Deserialize<RedisTemplate>(data);
    }

    /// <summary>
    /// Gets weather data for a given city.
    /// </summary>
    /// <param name="city">City name</param>
    /// <param name="hour">Time to be added to see forward forecast</param>
    /// <returns>Weather data</returns>
    public async Task<RedisTemplate?> GetWeatherByCityAndHourAsync(string city, int hour)
    {
        var key = $"{DateTime.Now.AddHours(hour + 1):ddMMyyhh}:{city}";
        var data = await _cache.GetStringAsync(key);
        
        return data == null ? null : JsonSerializer.Deserialize<RedisTemplate>(data);
    }
}