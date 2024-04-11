using System.Text.Json.Serialization;

namespace WeatherAPI.Models.CombinedWeather;
public record CombinedWeatherDataModel
{
    [JsonPropertyName("apisource")]
    public required string ApiSource { get; set; }

    [JsonPropertyName("temperature")]
    public required CombinedWeatherTemperatureModel CombinedWeatherTemperatureModel { get; set; }

    [JsonPropertyName("coord")]
    public required CombinedWeatherCoordModel CombinedWeatherCoordModel { get; set; }

    [JsonPropertyName("condition")]
    public required CombinedWeatherConditionModel CombinedWeatherConditionModel { get; set; }

    [JsonPropertyName("location")]
    public required CombinedWeatherLocationModel CombinedWeatherLocationModel { get; set; }
}
