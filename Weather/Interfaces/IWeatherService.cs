using DataTemplates;

namespace Weather.Interfaces;

public interface IWeatherService
{
    /// <summary>
    /// Gets weather data for a given city.
    /// </summary>
    /// <param name="city">City name</param>
    /// <returns>Weather data</returns>
    Task<RedisTemplate?> GetWeatherByCityAsync(string city);

    /// <summary>
    /// Gets weather data for a given city.
    /// </summary>
    /// <param name="city">City name</param>
    /// <param name="hour">Time to be added to see forward forecast</param>
    /// <returns>Weather data</returns>
    Task<RedisTemplate?> GetWeatherByCityAndHourAsync(string city, int hour);
}