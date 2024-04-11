using System.Text.Json.Serialization;

namespace WeatherAPI.Models.CombinedWeather;

[Serializable]
public record CombinedWeatherTemperatureModel
{
    [JsonPropertyName("kelvin")]
    public required float Kelvin { get; init; }

    [JsonPropertyName("feels_like_kelvin")]
    public required float FeelsLikeKelvin { get; init; }

    [JsonPropertyName("pressure_millibar")]
    public required float PressureMillibar { get; init; }

    [JsonPropertyName("humidity")]
    public required int Humidity { get; init; }
}
