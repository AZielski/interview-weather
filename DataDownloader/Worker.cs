using System.Text;
using System.Text.Json;
using DataDownloader.Helpers;
using DataTemplates;
using Microsoft.Extensions.Caching.Distributed;
using Mapper = DataDownloader.Helpers.Mapper;

namespace DataDownloader;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Timer _timer;
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

        if (data.Count == 0)
        {
            _logger.Log(LogLevel.Information, "No data fetched");
            return;
        }
        
        foreach (var cityDetails in data)
        {
            if (cityDetails is null)
            {
                continue;
            }

            var hourlyList = cityDetails.Data.Weather.Select(x => x.Hourly);
            var date = DateTime.Parse(cityDetails.Data.CurrentCondition.First().ObservationTime);
            var toSave = new List<RedisTemplate>();

            _logger.Log(LogLevel.Information, $"Parsing data for {cityDetails.Data.Request[0].Query}");
            foreach (var item in hourlyList)
            {
                toSave.AddRange(Mapper.MapperInstance.Map<List<RedisTemplate>>(item));
            }

            _logger.Log(LogLevel.Information, "Saving data to redis");
            foreach (var item in toSave)
            {
                item.City = cityDetails.Data.Request[0].Query.Split(',')[0];
                item.Date = date.AddHours(item.Time / 100).AddMinutes(-date.Minute);
                item.ObservationTime = data[0]!.Data.CurrentCondition.First().ObservationTime;
                
                await db.SetAsync($"{item.Date:ddMMyyhh}:{item.City}",
                    Encoding.ASCII.GetBytes(JsonSerializer.Serialize(item).ToCharArray()));
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