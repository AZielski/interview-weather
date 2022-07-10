using System.Text.Json.Serialization;

namespace DataTemplates;

[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
public class CurrentCondition
{
    [JsonPropertyName("observation_time")]
    public string ObservationTime { get; set; }

    [JsonPropertyName("temp_C")]
    public int TempC { get; set; }

    [JsonPropertyName("temp_F")]
    public int TempF { get; set; }

    [JsonPropertyName("weatherCode")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("weatherIconUrl")]
    public List<WeatherIconUrl> WeatherIconUrl { get; set; }

    [JsonPropertyName("weatherDesc")]
    public List<WeatherDesc> WeatherDesc { get; set; }

    [JsonPropertyName("windspeedMiles")]
    public int WindSpeedMiles { get; set; }

    [JsonPropertyName("windspeedKmph")]
    public int WindSpeedKmph { get; set; }

    [JsonPropertyName("winddirDegree")]
    public int WindDirDegree { get; set; }

    [JsonPropertyName("winddir16Point")]
    public string WindDir16Point { get; set; }

    [JsonPropertyName("precipMM")]
    public decimal PrecipMM { get; set; }

    [JsonPropertyName("precipInches")]
    public decimal PrecipInches { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("visibilityMiles")]
    public int VisibilityMiles { get; set; }

    [JsonPropertyName("pressure")]
    public int Pressure { get; set; }

    [JsonPropertyName("pressureInches")]
    public int PressureInches { get; set; }

    [JsonPropertyName("cloudcover")]
    public int CloudCover { get; set; }

    [JsonPropertyName("FeelsLikeC")]
    public int FeelsLikeC { get; set; }

    [JsonPropertyName("FeelsLikeF")]
    public int FeelsLikeF { get; set; }

    [JsonPropertyName("uvIndex")]
    public int UvIndex { get; set; }
}