using System.Text.Json.Serialization;

namespace WeatherAPI.Models.OpenWeather;

[Serializable]
public record OpenWeatherDataCloudsModel
{
    [JsonPropertyName("all")]
    public required int All { get; init; }
}
