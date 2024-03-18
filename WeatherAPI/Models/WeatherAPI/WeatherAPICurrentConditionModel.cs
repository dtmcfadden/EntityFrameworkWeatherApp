using System.Text.Json.Serialization;

namespace WeatherAPI.Models.WeatherAPI;

[Serializable]
public record WeatherAPICurrentConditionModel
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    [JsonPropertyName("icon")]
    public required string Icon { get; init; }

    [JsonPropertyName("code")]
    public required int Code { get; init; }
}
