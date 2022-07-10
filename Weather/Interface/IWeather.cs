using DataTemplates;

namespace Weather.Interface;

public interface IWeather
{
    Task<RedisTemplate?> GetWeatherAsync(string city);
}