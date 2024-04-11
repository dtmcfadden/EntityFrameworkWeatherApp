using System.Text.Json.Serialization;

namespace WeatherAPI.Models.CombinedWeather;

[Serializable]
public record CombinedWeatherLocationModel
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("localtime")]
    public required int LocalTime { get; init; }
}
