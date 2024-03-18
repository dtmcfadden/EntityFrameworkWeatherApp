using System.Text.Json.Serialization;

namespace WeatherAPI.Models.OpenWeather;

[Serializable]
public record OpenWeatherErrorModel
{
    [JsonPropertyName("cod")]
    public required int Code { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }

    public override string ToString()
    {
        return Code + ": " + Message;
    }
}
