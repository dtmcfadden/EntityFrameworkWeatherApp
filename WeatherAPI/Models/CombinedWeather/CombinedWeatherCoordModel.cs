using System.Text.Json.Serialization;

namespace WeatherAPI.Models.CombinedWeather;

[Serializable]
public record CombinedWeatherCoordModel
{
    [JsonPropertyName("lat")]
    public required float Latitude { get; init; }

    [JsonPropertyName("lon")]
    public required float Longitude { get; init; }
}
