using System.Text.Json.Serialization;

namespace DataTemplates;

public class Data
{
    [JsonPropertyName("request")]
    public List<Request> Request { get; set; }

    [JsonPropertyName("current_condition")]
    public List<CurrentCondition> CurrentCondition { get; set; }

    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; }
}

public class Root
{
    [JsonPropertyName("data")]
    public Data Data { get; set; }
}