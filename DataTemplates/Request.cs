using System.Text.Json.Serialization;

namespace DataTemplates;

public class Request
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("query")]
    public string Query { get; set; }
}