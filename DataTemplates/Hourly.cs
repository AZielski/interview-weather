namespace DataTemplates;

public class Hourly
{
    public string time { get; set; }
    public string tempC { get; set; }
    public string tempF { get; set; }
    public string windspeedMiles { get; set; }
    public string windspeedKmph { get; set; }
    public string winddirDegree { get; set; }
    public string winddir16Point { get; set; }
    public string weatherCode { get; set; }
    public List<WeatherIconUrl> weatherIconUrl { get; set; }
    public List<WeatherDesc> weatherDesc { get; set; }
    public string precipMM { get; set; }
    public string precipInches { get; set; }
    public string humidity { get; set; }
    public string visibility { get; set; }
    public string visibilityMiles { get; set; }
    public string pressure { get; set; }
    public string pressureInches { get; set; }
    public string cloudcover { get; set; }
    public string HeatIndexC { get; set; }
    public string HeatIndexF { get; set; }
    public string DewPointC { get; set; }
    public string DewPointF { get; set; }
    public string WindChillC { get; set; }
    public string WindChillF { get; set; }
    public string WindGustMiles { get; set; }
    public string WindGustKmph { get; set; }
    public string FeelsLikeC { get; set; }
    public string FeelsLikeF { get; set; }
    public string chanceofrain { get; set; }
    public string chanceofremdry { get; set; }
    public string chanceofwindy { get; set; }
    public string chanceofovercast { get; set; }
    public string chanceofsunshine { get; set; }
    public string chanceoffrost { get; set; }
    public string chanceofhightemp { get; set; }
    public string chanceoffog { get; set; }
    public string chanceofsnow { get; set; }
    public string chanceofthunder { get; set; }
    public string uvIndex { get; set; }
}