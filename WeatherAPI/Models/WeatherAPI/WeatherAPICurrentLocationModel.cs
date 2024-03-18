using System.Text.Json.Serialization;

namespace WeatherAPI.Models.WeatherAPI;

[Serializable]
public record WeatherAPICurrentLocationModel
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("region")]
    public required string Region { get; init; }

    [JsonPropertyName("country")]
    public required string Country { get; init; }

    [JsonPropertyName("lat")]
    public required float Latitude { get; init; }

    [JsonPropertyName("lon")]
    public required float Longitude { get; init; }

    [JsonPropertyName("tz_id")]
    public required string TZ_Id { get; init; }

    [JsonPropertyName("localtime_epoch")]
    public required int LocaltimeEpoch { get; init; }

    [JsonPropertyName("localtime")]
    public required string LocalTime { get; init; }

}
