using System.Text.Json.Serialization;

namespace WeatherAPI.Options;
public sealed record EnvironmentOptions
{
    [JsonPropertyName("openweather-apikey")]
    public required string OpenWeatherApiKey { get; set; }

    [JsonPropertyName("weatherapi-apikey")]
    public required string WeatherAPIApiKey { get; set; }

    [JsonPropertyName("weather-databasename")]
    public required string WeatherDatabaseName { get; set; }

    [JsonPropertyName("weather-connectionstring")]
    public required string WeatherConnectionString { get; set; }
}
