namespace RedisConnectorHelper.DataTemplates;

public class Weather
{
    public string date { get; set; }
    public List<Astronomy> astronomy { get; set; }
    public string maxtempC { get; set; }
    public string maxtempF { get; set; }
    public string mintempC { get; set; }
    public string mintempF { get; set; }
    public string avgtempC { get; set; }
    public string avgtempF { get; set; }
    public string totalSnow_cm { get; set; }
    public string sunHour { get; set; }
    public string uvIndex { get; set; }
    public List<Hourly> hourly { get; set; }
}

public class WeatherDesc
{
    public string value { get; set; }
}

public class WeatherIconUrl
{
    public string value { get; set; }
}