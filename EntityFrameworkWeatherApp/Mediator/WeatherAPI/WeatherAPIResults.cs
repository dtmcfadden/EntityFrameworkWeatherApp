using EntityFrameworkWeatherApp.Abstractions.Results;
using WeatherAPI.Models.WeatherAPI;
using WeatherAPI.Requests.Queries.WeatherAPI;

namespace EntityFrameworkWeatherApp.Mediator.WeatherAPI;

public class WeatherAPIResults
{
    public static async Task<Result<WeatherAPICurrentModel?>> GetWeatherAPIWeatherByLatLongResult(float latitude, float longitude, ISender sender)
    {
        var query = new GetWeatherAPIWeatherByLatLongQuery(latitude, longitude);

        var result = await sender.Send(query);
        return result;
    }

    public static async Task<Result<WeatherAPICurrentModel?>> GetWeatherAPIWeatherByLocationNameResult(string location, ISender sender)
    {
        var query = new GetWeatherAPIWeatherByLocationNameQuery(location);

        var result = await sender.Send(query);
        return result;
    }
}
