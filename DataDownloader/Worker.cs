using System.Text.Json;
using DataDownloader.DataTemplates;
using StackExchange.Redis;

namespace DataDownloader;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly Timer _timer;
    
    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        
        var now = DateTime.Now;
        var whenToFetch = new DateTime(now.Year, now.Month, now.Day + 1, 6, 0, 0);
        var timeToWait = (whenToFetch - now).TotalMilliseconds;
        
        _timer = new Timer(Job, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(timeToWait));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Log(LogLevel.Information, "Worker is starting.");
        Job(null);
    }

    private void Job(object? o)
    {
        _logger.Log(LogLevel.Information, "Connecting to Redis");
        var redis = ConnectionMultiplexer.Connect(_configuration["Redis:ConnectionString"]);
        var db = redis.GetDatabase();

        _logger.Log(LogLevel.Information, "Downloading data");
        var json = File.ReadAllText("DataSample.json");
        var data = JsonSerializer.Deserialize<Root>(json);

        if (data is null)
        {
            _logger.LogError("Data is null, closing connection and stopping job");
            redis.Close();
            return;
        }
        
        _logger.Log(LogLevel.Information, "Clearing Redis");
        db.KeyDelete("*");
        
        var c = data.data.weather.Select(x => new { x.hourly, x.date });
        var toSave = new List<RedisTemplate>();

        _logger.Log(LogLevel.Information, "Processing data");
        foreach (var item in c)
        {
            foreach (var hourly in item.hourly)
            {
                var rt = new RedisTemplate
                {
                    tempC = hourly.tempC,
                    tempF = hourly.tempF,
                    windspeedMiles = hourly.windspeedMiles,
                    windspeedKmph = hourly.windspeedKmph,
                    winddirDegree = hourly.winddirDegree,
                    winddir16Point = hourly.winddir16Point,
                    weatherCode = hourly.weatherCode,
                    weatherIconUrl = hourly.weatherIconUrl,
                    weatherDesc = hourly.weatherDesc,
                    precipMM = hourly.precipMM,
                    precipInches = hourly.precipInches,
                    humidity = hourly.humidity,
                    visibility = hourly.visibility,
                    visibilityMiles = hourly.visibilityMiles,
                    pressure = hourly.pressure,
                    pressureInches = hourly.pressureInches,
                    cloudcover = hourly.cloudcover,
                    HeatIndexC = hourly.HeatIndexC,
                    HeatIndexF = hourly.HeatIndexF,
                    DewPointC = hourly.DewPointC,
                    DewPointF = hourly.DewPointF,
                    WindChillC = hourly.WindChillC,
                    WindChillF = hourly.WindChillF,
                    WindGustMiles = hourly.WindGustMiles,
                    WindGustKmph = hourly.WindGustKmph,
                    FeelsLikeC = hourly.FeelsLikeC,
                    FeelsLikeF = hourly.FeelsLikeF,
                    chanceofrain = hourly.chanceofrain,
                    chanceofremdry = hourly.chanceofremdry,
                    chanceofwindy = hourly.chanceofwindy,
                    chanceofovercast = hourly.chanceofovercast,
                    chanceofsunshine = hourly.chanceofsunshine,
                    chanceoffrost = hourly.chanceoffrost,
                    chanceofhightemp = hourly.chanceofhightemp,
                    chanceoffog = hourly.chanceoffog,
                    chanceofsnow = hourly.chanceofsnow,
                    chanceofthunder = hourly.chanceofthunder,
                    uvIndex = hourly.uvIndex,
                    City = data.data.request[0].query.Split(',')[0],
                    Date = DateTime.Parse(item.date)
                };

                var startTime = TimeOnly.Parse(data.data.current_condition[0].observation_time)
                    .AddHours(int.Parse(hourly.time) / 100);
                rt.Date = rt.Date.AddHours(startTime.Hour);
                toSave.Add(rt);
            }
        }

        _logger.Log(LogLevel.Information, "Saving data");
        foreach (var item in toSave)
        {
            db.SetAdd($"{item.Date:ddMMyy}:{item.City}", JsonSerializer.Serialize(item));
        }
        
        _logger.Log(LogLevel.Information, "Work done, closing connection");
        redis.Close();
    }
}