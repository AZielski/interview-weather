using System.Text;
using System.Text.Json;
using DataDownloader.Helpers;
using DataTemplates;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
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
        
        _logger.Log(LogLevel.Information, "Processing data");
        foreach (var cityDetails in data.Where(cityDetails => cityDetails is not null))
        {
            await ProcessDataAsync(cityDetails!, db);
        }

        _logger.Log(LogLevel.Information, "Work done, closing connection");
    }

    /// <summary>
    /// Process data and saves them into redis cache
    /// </summary>
    /// <param name="cityDetails">City details to be saved</param>
    /// <param name="db">Redis connection</param>
    private async Task ProcessDataAsync(Root cityDetails, IDistributedCache db)
    {
        var hourlyList = cityDetails.Data.Weather.Select(x => x.Hourly);
        var date = DateTime.Parse(cityDetails.Data.CurrentCondition.First().ObservationTime);
        var toSave = new List<RedisTemplate>();

        foreach (var item in hourlyList)
        {
            toSave.AddRange(Mapper.MapperInstance.Map<List<RedisTemplate>>(item));
        }

        foreach (var item in toSave)
        {
            item.City = cityDetails.Data.Request[0].Query.Split(',')[0];
            item.Date = date.AddHours(item.Time / 100).AddMinutes(-date.Minute);
            item.ObservationTime = $"{date:s}";
                
            await db.SetAsync($"{item.Date:ddMMyyhh}:{item.City}",
                Encoding.ASCII.GetBytes(JsonSerializer.Serialize(item).ToCharArray()));
        }
    }
    
    /// <summary>
    /// Fetch data from API and deserialize them
    /// </summary>
    /// <param name="city">City name for which weather should be fetched</param>
    /// <returns>Fetched data as Root object</returns>
    private async Task<Root?> GetDataForCity(string city)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(_configuration["query"].Replace("#CITY#", city));
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Root>(json);
    }
}