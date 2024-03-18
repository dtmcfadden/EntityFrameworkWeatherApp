using System.Text.Json.Serialization;

namespace WeatherAPI.Models.WeatherAPI;

[Serializable]
public record WeatherAPICurrentCurrentModel
{
    [JsonPropertyName("last_updated_epoch")]
    public required int LastUpdatedEpoch { get; init; }

    [JsonPropertyName("last_updated")]
    public required string LastUpdated { get; init; }

    [JsonPropertyName("temp_c")]
    public required float TempC { get; init; }

    [JsonPropertyName("temp_f")]
    public required float TempF { get; init; }

    [JsonPropertyName("is_day")]
    public required int IsDay { get; init; }

    [JsonPropertyName("condition")]
    public required WeatherAPICurrentConditionModel Condition { get; init; }

    [JsonPropertyName("wind_mph")]
    public required float WindMph { get; init; }

    [JsonPropertyName("wind_kph")]
    public required float WindKph { get; init; }

    [JsonPropertyName("wind_degree")]
    public required int WindDegree { get; init; }

    [JsonPropertyName("wind_dir")]
    public required string WindDir { get; init; }

    [JsonPropertyName("pressure_mb")]
    public required float PressureMb { get; init; }

    [JsonPropertyName("pressure_in")]
    public required float PressureIn { get; init; }

    [JsonPropertyName("precip_mm")]
    public required float PrecipMm { get; init; }

    [JsonPropertyName("precip_in")]
    public required float PrecipIn { get; init; }

    [JsonPropertyName("humidity")]
    public required int Humidity { get; init; }

    [JsonPropertyName("cloud")]
    public required int Cloud { get; init; }

    [JsonPropertyName("feelslike_c")]
    public required float FeelsLikeC { get; init; }

    [JsonPropertyName("feelslike_f")]
    public required float FeelsLikeF { get; init; }

    [JsonPropertyName("vis_km")]
    public required float VisibilityKm { get; init; }

    [JsonPropertyName("vis_miles")]
    public required float VisibilityMiles { get; init; }

    [JsonPropertyName("uv")]
    public required float Ultraviolet { get; init; }

    [JsonPropertyName("gust_mph")]
    public required float GustMph { get; init; }

    [JsonPropertyName("gust_kph")]
    public required float GustKph { get; init; }
}
