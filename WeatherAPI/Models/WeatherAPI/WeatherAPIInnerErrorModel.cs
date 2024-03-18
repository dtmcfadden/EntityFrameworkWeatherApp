using System.Text.Json.Serialization;

namespace WeatherAPI.Models.WeatherAPI;

public record WeatherAPIInnerErrorModel
{
    [JsonPropertyName("code")]
    public required int Code { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }
}
