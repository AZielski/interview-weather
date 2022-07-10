using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IDistributedCache _cache;

    public WeatherController(ILogger<WeatherController> logger, IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }
    
    [HttpGet("{cityName}")]
    public async Task<string?> Get(string cityName)
    {
        //Get data from cache
        var key = $"{DateTime.UtcNow:ddMMyy}:{cityName}";
        _cache.Set(key, Encoding.ASCII.GetBytes("essa"));
        var cachedData = await _cache.GetStringAsync("090722:Warsaw");
        
        //get set from redis
        

        return cachedData ?? "Done";
    }
}