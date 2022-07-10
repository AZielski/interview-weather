using System.Text.Json.Serialization;

namespace DataTemplates;

[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
public class Hourly
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("heatIndexC")]
        public int HeatIndexC { get; set; }

        [JsonPropertyName("heatIndexF")]
        public int HeatIndexF { get; set; }

        [JsonPropertyName("dewPointC")]
        public int DewPointC { get; set; }

        [JsonPropertyName("dewPointF")]
        public int DewPointF { get; set; }

        [JsonPropertyName("windChillC")]
        public int WindChillC { get; set; }

        [JsonPropertyName("windChillF")]
        public int WindChillF { get; set; }

        [JsonPropertyName("windGustMiles")]
        public int WindGustMiles { get; set; }

        [JsonPropertyName("windGustKmph")]
        public int WindGustKmph { get; set; }

        [JsonPropertyName("chanceofrain")]
        public int ChanceOfRain { get; set; }

        [JsonPropertyName("chanceofremdry")]
        public int ChanceOfRemdry { get; set; }

        [JsonPropertyName("chanceofwindy")]
        public int ChanceOfWindy { get; set; }

        [JsonPropertyName("chanceofovercast")]
        public int ChanceOfOvercast { get; set; }

        [JsonPropertyName("chanceofsunshine")]
        public int ChanceOfSunshine { get; set; }

        [JsonPropertyName("chanceoffrost")]
        public int ChanceOfFrost { get; set; }

        [JsonPropertyName("chanceofhightemp")]
        public int ChanceOfHightemp { get; set; }

        [JsonPropertyName("chanceoffog")]
        public int ChanceOfFog { get; set; }

        [JsonPropertyName("chanceofsnow")]
        public int ChanceOfSnow { get; set; }

        [JsonPropertyName("chanceofthunder")]
        public int ChanceOfThunder { get; set; }

        [JsonPropertyName("uvIndex")]
        public int UvIndex { get; set; }

        [JsonPropertyName("tempC")]
        public int TempC { get; set; }

        [JsonPropertyName("tempF")]
        public int TempF { get; set; }

        [JsonPropertyName("windspeedMiles")]
        public int WindSpeedMiles { get; set; }

        [JsonPropertyName("windspeedKmph")]
        public int WindSpeedKmph { get; set; }

        [JsonPropertyName("winddirDegree")]
        public int WindDirDegree { get; set; }

        [JsonPropertyName("winddir16Point")]
        public string WindDir16Point { get; set; }

        [JsonPropertyName("weatherCode")]
        public int WeatherCode { get; set; }

        [JsonPropertyName("weatherIconUrl")]
        public List<WeatherIconUrl> WeatherIconUrl { get; set; }

        [JsonPropertyName("weatherDesc")]
        public List<WeatherDesc> WeatherDesc { get; set; }

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
        
        public string ObservationTime { get; set; }
    }