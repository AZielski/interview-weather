using System.Text;
using System.Text.Json;
using AutoMapper;
using DataTemplates;
using Microsoft.Extensions.Caching.Distributed;

namespace DataDownloader;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Timer _timer;
    private readonly Mapper _mapper;
    private readonly List<string> _cities;
    private readonly IConfiguration _configuration;
    
    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _cities = configuration.GetSection("Cities").Get<List<string>>();
        _configuration = configuration;
        
        var now = DateTime.Now;
        var whenToFetch = new DateTime(now.Year, now.Month, now.Day + 1, 6, 0, 0);
        var timeToWait = (whenToFetch - now).TotalMilliseconds;
        
        _timer = new Timer(Job, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(timeToWait));
        _mapper = new Mapper(new MapperConfiguration( m =>
        {
            m.CreateMap<Hourly, RedisTemplate>();
        }));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Log(LogLevel.Information, "Worker is starting.");
    }

    private async void Job(object? o)
    {
        _logger.Log(LogLevel.Information, "Connecting to redis");
        using var redis = new RedisConnector();
        var db = redis.ConnectToRedis();
        
        _logger.Log(LogLevel.Information, "Downloading data");
        var data = new List<Root?>();

        foreach (var city in _cities)
        {
            var temp = await GetDataForCity(city);
            data.Add(temp);
        }

        foreach (var cityDetails in data)
        {
            if (cityDetails is null)
            {
                continue;
            }
            
            var hourlyList = cityDetails.data.weather.Select(x => x.hourly);
            var date = cityDetails.data.current_condition.First().observation_time;
            var toSave = new List<RedisTemplate>();

            _logger.Log(LogLevel.Information, $"Parsing data for {cityDetails.data.request[0].query}");
            foreach (var item in hourlyList)
            {
                toSave.AddRange(_mapper.Map<List<RedisTemplate>>(item));
            }

            _logger.Log(LogLevel.Information, "Saving data to redis");
            foreach (var item in toSave)
            {
                item.City = cityDetails.data.request[0].query.Split(',')[0];
                item.Date = DateTime.Parse(date).AddHours(int.Parse(item.time) / 100);

                db.Set($"{item.Date:ddMMyyhh}:{item.City}", Encoding.ASCII.GetBytes(JsonSerializer.Serialize(item).ToCharArray()), new DistributedCacheEntryOptions());
            }
        }
        
        _logger.Log(LogLevel.Information, "Work done, closing connection");
    }
    
    private async Task<Root?> GetDataForCity(string city)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(_configuration["query"].Replace("#CITY#", city));
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Root>(json);
    }
}