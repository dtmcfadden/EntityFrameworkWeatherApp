using WeatherAPI.Models.OpenWeather;
using WeatherAPI.UnitTests.MockData.ModelData;

namespace WeatherAPI.UnitTests.MockData.Services;
internal sealed class OpenWeatherHTTPServiceMockData
{
    public static Result<OpenWeatherDataModel?> GetWeatherByLatLong(string name)
    {
        var returnData = GetOpenWeatherDataModel[name];

        return returnData;
    }

    private static readonly Dictionary<string, Result<OpenWeatherDataModel?>> GetOpenWeatherDataModel = new()
    {
        { "SuccessLondon", new Result<OpenWeatherDataModel?> (OpenWeatherDataModelMockData.GetOpenWeatherDataModel("London") ) }
    };

    public static Result<List<OpenWeatherGeoDirectModel>?>? GetGeoDirect(string name)
    {
        var returnData = GetOpenWeatherGeoDirectModel[name];

        return returnData;
    }

    private static readonly Dictionary<string, Result<List<OpenWeatherGeoDirectModel>?>?> GetOpenWeatherGeoDirectModel = new()
    {
        { "SuccessLondon",
            new Result<List<OpenWeatherGeoDirectModel>?> (OpenWeatherDataModelMockData.GetOpenWeatherGeoDirectModel("London") )}
    };

    public static Result<OpenWeatherGeoZipModel?> GetGeoZip(string name)
    {
        var returnData = GetOpenWeatherGeoZipModel[name];

        return returnData;
    }

    private static readonly Dictionary<string, Result<OpenWeatherGeoZipModel?>> GetOpenWeatherGeoZipModel = new()
    {
        { "Success55407", new Result<OpenWeatherGeoZipModel?> (OpenWeatherDataModelMockData.GetOpenWeatherGeoZipModel("55407") ) }
    };
}
