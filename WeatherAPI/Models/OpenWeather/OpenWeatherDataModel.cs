using System.Text.Json.Serialization;

namespace WeatherAPI.Models.OpenWeather;

[Serializable]
public record OpenWeatherDataModel
{
    [JsonPropertyName("coord")]
    public required OpenWeatherDataCoordModel Coordinates { get; init; }

    [JsonPropertyName("weather")]
    public required List<OpenWeatherDataWeatherModel> Weather { get; init; }

    [JsonPropertyName("base")]
    public required string BaseType { get; init; }

    [JsonPropertyName("main")]
    public required OpenWeatherDataMainModel Main { get; init; }

    [JsonPropertyName("visibility")]
    public required int Visibility { get; init; }

    [JsonPropertyName("wind")]
    public OpenWeatherDataWindModel? Wind { get; init; }

    [JsonPropertyName("rain")]
    public OpenWeatherDataRainModel? Rain { get; init; }

    [JsonPropertyName("snow")]
    public OpenWeatherDataSnowModel? Snow { get; init; }

    [JsonPropertyName("clouds")]
    public OpenWeatherDataCloudsModel? Clouds { get; init; }

    [JsonPropertyName("dt")]
    public required int Dt { get; init; }

    [JsonPropertyName("sys")]
    public required OpenWeatherDataSysModel Sys { get; init; }

    [JsonPropertyName("timezone")]
    public required int Timezone { get; init; }

    [JsonPropertyName("id")]
    public int? CityId { get; init; }

    [JsonPropertyName("name")]
    public string? CityName { get; init; }

    [JsonPropertyName("cod")]
    public required int Cod { get; init; }
}
