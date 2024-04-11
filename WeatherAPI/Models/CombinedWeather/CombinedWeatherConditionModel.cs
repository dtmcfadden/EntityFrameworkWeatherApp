using System.Text.Json.Serialization;

namespace WeatherAPI.Models.CombinedWeather;

[Serializable]
public record CombinedWeatherConditionModel
{
    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("icon")]
    public required string Icon { get; init; }

    [JsonPropertyName("visibility")]
    public required float Visibility { get; init; }

    [JsonPropertyName("wind_speed")]
    public required float? WindSpeed { get; init; }

    [JsonPropertyName("wind_degree")]
    public required int? WindDegree { get; init; }

    [JsonPropertyName("wind_gust")]
    public required float? WindGust { get; init; }

    [JsonPropertyName("precip_mm")]
    public required float? PrecipitationMm { get; init; }

    [JsonPropertyName("clouds")]
    public required int? Clouds { get; init; }
}
