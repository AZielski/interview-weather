using DataTemplates;
using Microsoft.AspNetCore.Mvc;
using Weather.Interfaces;

namespace Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    
    [HttpGet("{cityName}")]
    public async Task<RedisTemplate?> GetWeatherByCityAsync(string cityName)
    {
        return await _weatherService.GetWeatherByCityAsync(cityName);
    }
    
    [HttpGet("{cityName}/{hour:int}")]
    public async Task<RedisTemplate?> GetWeatherByCityAndHourAsync(string cityName, int hour)
    {
        return await _weatherService.GetWeatherByCityAndHourAsync(cityName, hour);
    }
}