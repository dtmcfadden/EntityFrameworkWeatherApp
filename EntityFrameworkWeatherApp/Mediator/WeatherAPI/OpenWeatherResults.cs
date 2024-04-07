using EntityFrameworkWeatherApp.Common;
using EntityFrameworkWeatherApp.Domain.Entities;
using EntityFrameworkWeatherApp.Infrastructure.Results;
using WeatherAPI.Models.OpenWeather;
using WeatherAPI.Requests.Queries.OpenWeather;

namespace EntityFrameworkWeatherApp.Mediator.WeatherAPI;

public static class OpenWeatherResults
{
    public static async Task<Result<List<OpenWeatherGeoDirectModel>>> GetOpenWeatherGeoDirectResult(
        string location,
        ISender sender, HttpTrackingEntity httpTracking)
    {
        var query = new GetOpenWeatherGeoDirectQuery(location);
        var result = await sender.Send(query);

        WeatherCommonStatic.AddWeatherEnpointHistory(sender, httpTracking, location);

        return result;
    }

    public static async Task<Result<OpenWeatherGeoZipModel>> GetOpenWeatherGeoZipResult(
        string zip,
        ISender sender, HttpTrackingEntity httpTracking)
    {
        var query = new GetOpenWeatherGeoZipQuery(zip);
        var result = await sender.Send(query);

        WeatherCommonStatic.AddWeatherEnpointHistory(sender, httpTracking, zip);

        return result;
    }

    public static async Task<Result<OpenWeatherDataModel>> GetOpenWeatherDirectLatLongResult(
        float latitude, float longitude,
        ISender sender, HttpTrackingEntity httpTracking)
    {
        var query = new GetOpenWeatherWeatherByLatLongQuery(latitude, longitude);
        var result = await sender.Send(query);

        WeatherCommonStatic.AddWeatherEnpointHistory(sender, httpTracking, latitude, longitude);

        return result;
    }

    public static async Task<Result<OpenWeatherDataModel>> GetOpenWeatherDirectLocationResult(
        string location,
        ISender sender, HttpTrackingEntity httpTracking)
    {
        var query = new GetOpenWeatherWeatherByLocationNameQuery(location);
        var result = await sender.Send(query);

        WeatherCommonStatic.AddWeatherEnpointHistory(sender, httpTracking, location);

        return result;
    }
}
