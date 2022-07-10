namespace RedisConnectorHelper.DataTemplates;

public class CurrentCondition
{
    public string observation_time { get; set; }
    public string temp_C { get; set; }
    public string temp_F { get; set; }
    public string weatherCode { get; set; }
    public List<WeatherIconUrl> weatherIconUrl { get; set; }
    public List<WeatherDesc> weatherDesc { get; set; }
    public string windspeedMiles { get; set; }
    public string windspeedKmph { get; set; }
    public string winddirDegree { get; set; }
    public string winddir16Point { get; set; }
    public string precipMM { get; set; }
    public string precipInches { get; set; }
    public string humidity { get; set; }
    public string visibility { get; set; }
    public string visibilityMiles { get; set; }
    public string pressure { get; set; }
    public string pressureInches { get; set; }
    public string cloudcover { get; set; }
    public string FeelsLikeC { get; set; }
    public string FeelsLikeF { get; set; }
    public string uvIndex { get; set; }
}