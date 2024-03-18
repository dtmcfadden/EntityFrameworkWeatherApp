using System.Text.Json.Serialization;

namespace WeatherAPI.Models.OpenWeather;

[Serializable]
public record OpenWeatherGeoDirectModel
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("local_names")]
    public Dictionary<string, string>? Local_Names { get; init; } = null;

    [JsonPropertyName("lat")]
    public required float Latitude { get; init; }

    [JsonPropertyName("lon")]
    public required float Longitude { get; init; }

    [JsonPropertyName("country")]
    public required string Country { get; init; }

    [JsonPropertyName("state")]
    public string? State { get; init; } = null;
}
