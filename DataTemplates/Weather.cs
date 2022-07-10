using System.Text.Json.Serialization;

namespace DataTemplates;

[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
public class Weather
{
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("astronomy")]
    public List<Astronomy> Astronomy { get; set; }

    [JsonPropertyName("maxtempC")]
    public int MaxTempC { get; set; }

    [JsonPropertyName("maxtempF")]
    public int MaxTempF { get; set; }

    [JsonPropertyName("mintempC")]
    public int MinTempC { get; set; }

    [JsonPropertyName("mintempF")]
    public int MinTempF { get; set; }

    [JsonPropertyName("avgtempC")]
    public int AvgTempC { get; set; }

    [JsonPropertyName("avgtempF")]
    public int AvgTempF { get; set; }

    [JsonPropertyName("totalSnow_cm")]
    public decimal TotalSnowCm { get; set; }

    [JsonPropertyName("sunHour")]
    public string SunHour { get; set; }

    [JsonPropertyName("uvIndex")]
    public int UvIndex { get; set; }

    [JsonPropertyName("hourly")]
    public List<Hourly> Hourly { get; set; }
}

public class WeatherDesc
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class WeatherIconUrl
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
}