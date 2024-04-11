using EntityFrameworkWeatherApp.Common;
using EntityFrameworkWeatherApp.Domain.Entities;
using EntityFrameworkWeatherApp.Infrastructure.Results;
using WeatherAPI.Models.CombinedWeather;
using WeatherAPI.Requests.Queries.CombinedWeather;

namespace EntityFrameworkWeatherApp.Mediator.WeatherAPI;

public static class CombinedWeatherResults
{
    public static async Task<Result<CombinedWeatherDataModel>> GetCombinedWeatherLatLongResult(
        float latitude, float longitude,
        ISender sender, HttpTrackingEntity httpTracking)
    {
        var query = new GetCombinedWeatherByLatLongQuery(latitude, longitude);
        var result = await sender.Send(query);

        WeatherCommonStatic.AddWeatherEnpointHistory(sender, httpTracking, latitude, longitude);

        return result;
    }

    public static async Task<Result<CombinedWeatherDataModel>> GetCombinedWeatherLocationResult(
        string location,
        ISender sender, HttpTrackingEntity httpTracking)
    {
        var query = new GetCombinedWeatherByLocationNameQuery(location);
        var result = await sender.Send(query);

        WeatherCommonStatic.AddWeatherEnpointHistory(sender, httpTracking, location);

        return result;
    }
}
