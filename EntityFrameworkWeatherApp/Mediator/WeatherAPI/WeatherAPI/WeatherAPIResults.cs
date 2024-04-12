using EntityFrameworkWeatherApp.Common;
using EntityFrameworkWeatherApp.Domain.Entities;
using EntityFrameworkWeatherApp.Infrastructure.Results;
using WeatherAPI.Models.WeatherAPI;
using WeatherAPI.Requests.Queries.WeatherAPI;

namespace EntityFrameworkWeatherApp.Mediator.WeatherAPI.WeatherAPI;

public class WeatherAPIResults
{
    public static async Task<Result<WeatherAPICurrentModel>> GetWeatherAPIWeatherByLatLongResult(
        float latitude, float longitude,
        ISender sender, HttpTrackingEntity httpTracking)
    {
        var query = new GetWeatherAPIWeatherByLatLongQuery(latitude, longitude);
        var result = await sender.Send(query);

        WeatherCommonStatic.AddWeatherEnpointHistory(sender, httpTracking, latitude, longitude);

        return result;
    }

    public static async Task<Result<WeatherAPICurrentModel>> GetWeatherAPIWeatherByLocationNameResult(
        string location,
        ISender sender, HttpTrackingEntity httpTracking)
    {
        var query = new GetWeatherAPIWeatherByLocationNameQuery(location);
        var result = await sender.Send(query);

        WeatherCommonStatic.AddWeatherEnpointHistory(sender, httpTracking, location);

        return result;
    }
}
