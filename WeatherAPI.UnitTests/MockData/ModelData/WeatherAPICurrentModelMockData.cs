using WeatherAPI.Models.WeatherAPI;

namespace WeatherAPI.UnitTests.MockData.ModelData;
internal static class WeatherAPICurrentModelMockData
{
    public static WeatherAPICurrentModel GetWeatherAPICurrentModel(string name)
    {
        return WeatherAPICurrentModelList[name];
    }

    private static readonly Dictionary<string, WeatherAPICurrentModel> WeatherAPICurrentModelList = new()
    {
        { "London",
            new () {
                Location = new() {
                    Country = "United Kingdom",
                    Latitude = 51.52f,
                    Longitude = -0.11f,
                    LocalTime = "2024-03-17 14:23",
                    Name = "London",
                    LocaltimeEpoch = 1710685423,
                    Region = "City of London, Greater London",
                    TZ_Id = "Europe/London"
                },
                Current = new() {
                    Cloud = 69,
                    Condition = new()
                    {
                        Code = 1063,
                        Icon = "//cdn.weatherapi.com/weather/64x64/day/176.png",
                        Text = "Patchy rain nearby"
                    },
                    FeelsLikeC = 11.6f,
                    FeelsLikeF = 52.9f,
                    GustKph = 16.1f,
                    GustMph = 10,
                    Humidity = 89,
                    IsDay = 1,
                    LastUpdated = "2024-03-17 14:15",
                    LastUpdatedEpoch = 1710684900,
                    PrecipIn = 0,
                    PrecipMm = 0.02f,
                    PressureIn = 29.97f,
                    PressureMb = 1015,
                    TempC = 12.7f,
                    TempF = 54.9f,
                    Ultraviolet = 3,
                    VisibilityKm = 10,
                    VisibilityMiles = 6,
                    WindDegree = 198,
                    WindDir = "SSW",
                    WindKph = 12.2f,
                    WindMph= 7.6f
                },
            }
        }
    };
}
