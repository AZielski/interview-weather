using DataTemplates;
using Microsoft.AspNetCore.Mvc;
using Weather.Interface;

namespace Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeather _weather;

    public WeatherController(IWeather weather)
    {
        _weather = weather;
    }
    
    [HttpGet("{cityName}")]
    public async Task<RedisTemplate?> Get(string cityName)
    {
        return await _weather.GetWeatherAsync(cityName);
    }
}