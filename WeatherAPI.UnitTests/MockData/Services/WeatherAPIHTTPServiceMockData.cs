using WeatherAPI.Models.WeatherAPI;
using WeatherAPI.UnitTests.MockData.ModelData;

namespace WeatherAPI.UnitTests.MockData.Services;
internal sealed class WeatherAPIHTTPServiceMockData
{
    public static Result<WeatherAPICurrentModel> GetWeatherByLatLong(string name)
    {
        var returnData = GetWeatherAPICurrentModel[name];

        return returnData;
    }

    public static Result<WeatherAPICurrentModel> GetWeatherByLocationName(string name)
    {
        var returnData = GetWeatherAPICurrentModel[name];

        return returnData;
    }

    private static readonly Dictionary<string, Result<WeatherAPICurrentModel>> GetWeatherAPICurrentModel = new()
    {
        { "SuccessLondon",
            new Result<WeatherAPICurrentModel> (WeatherAPICurrentModelMockData.GetWeatherAPICurrentModel("London") ) }
    };
}
