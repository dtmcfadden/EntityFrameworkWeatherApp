using System.Text.Json.Serialization;

namespace WeatherAPI.Models.OpenWeather;

[Serializable]
public record OpenWeatherDataWeatherModel
{
    [JsonPropertyName("id")]
    public required int WeatherId { get; init; }

    [JsonPropertyName("main")]
    public required string Main { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("icon")]
    public required string Icon { get; init; }
}
