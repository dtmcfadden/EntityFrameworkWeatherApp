using System.Text.Json.Serialization;

namespace WeatherAPI.Models.OpenWeather;

[Serializable]
public record OpenWeatherDataMainModel
{
    [JsonPropertyName("temp")]
    public required float Temperature { get; init; }

    [JsonPropertyName("feels_like")]
    public required float FeelsLike { get; init; }

    [JsonPropertyName("temp_min")]
    public required float TemperatureMin { get; init; }

    [JsonPropertyName("temp_max")]
    public required float TemperatureMax { get; init; }

    [JsonPropertyName("pressure")]
    public required int Pressure { get; init; }

    [JsonPropertyName("humidity")]
    public required int Humidity { get; init; }

    [JsonPropertyName("sea_level")]
    public int? SeaLevel { get; init; }

    [JsonPropertyName("grnd_level")]
    public int? GroundLevel { get; init; }
}
