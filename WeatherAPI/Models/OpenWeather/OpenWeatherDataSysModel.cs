using System.Text.Json.Serialization;

namespace WeatherAPI.Models.OpenWeather;

[Serializable]
public record OpenWeatherDataSysModel
{
    [JsonPropertyName("type")]
    public int? Type { get; init; }

    [JsonPropertyName("id")]
    public int? SystemId { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("sunrise")]
    public required int Sunrise { get; init; }

    [JsonPropertyName("sunset")]
    public required int Sunset { get; init; }
}
