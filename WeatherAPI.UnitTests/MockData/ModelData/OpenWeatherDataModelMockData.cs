using WeatherAPI.Models.OpenWeather;

namespace WeatherAPI.UnitTests.MockData.ModelData;
internal static class OpenWeatherDataModelMockData
{
    public static OpenWeatherDataModel GetOpenWeatherDataModel(string name)
    {
        return OpenWeatherDataModelList[name];
    }

    private static readonly Dictionary<string, OpenWeatherDataModel> OpenWeatherDataModelList = new()
    {
        { "London",
            new () {
                BaseType = "stations",
                CityId = 2643743,
                CityName = "London",
                Cod = 200,
                Timezone = 0,
                Visibility = 10000,
                Dt = 1710519688,
                Coordinates = new() { Latitude = 51.5073f, Longitude = -0.1276f },
                Main = new() { Temperature = 286.84f, TemperatureMin = 285.87f, TemperatureMax = 287.77f,
                FeelsLike = 286.35f, Humidity = 80, Pressure = 1004 },
                Sys = new() { Sunrise = 1710483236, Sunset = 1710525891 },
                Weather = [ new() { WeatherId = 802, Main = "Clouds",
                    Description = "scattered clouds", Icon = "03d" } ]
            }
        }
    };

    public static List<OpenWeatherGeoDirectModel> GetOpenWeatherGeoDirectModel(string name)
    {
        return OpenWeatherGeoDirectModelList[name];
    }

    private static readonly Dictionary<string, List<OpenWeatherGeoDirectModel>> OpenWeatherGeoDirectModelList = new()
    {
        { "London",
            [new () {
                Name = "London",
                Local_Names = new() { {"gc", "Lunnin" }, { "feature_name", "London" } },
                Latitude = 51.50732f,
                Longitude = -0.1276474f,
                Country = "GB"
            }]
        }
    };

    public static OpenWeatherGeoZipModel GetOpenWeatherGeoZipModel(string name)
    {
        return OpenWeatherGeoZipModelList[name];
    }

    private static readonly Dictionary<string, OpenWeatherGeoZipModel> OpenWeatherGeoZipModelList = new()
    {
        { "55407",
            new () {
                Zip = "55407",
                Name = "Minneapolis",
                Latitude = 44.9378f,
                Longitude = -93.2545f,
                Country = "US"
            }
        }
    };
}
