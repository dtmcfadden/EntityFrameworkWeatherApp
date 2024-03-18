using System.Text.Json.Serialization;

namespace WeatherAPI.Models.WeatherAPI;

[Serializable]
public record WeatherAPICurrentModel
{
    [JsonPropertyName("location")]
    public required WeatherAPICurrentLocationModel Location { get; init; }

    [JsonPropertyName("current")]
    public required WeatherAPICurrentCurrentModel Current { get; init; }
}
