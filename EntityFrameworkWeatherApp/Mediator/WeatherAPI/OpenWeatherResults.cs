using EntityFrameworkWeatherApp.Abstractions.Results;
using WeatherAPI.Models.OpenWeather;
using WeatherAPI.Requests.Queries.OpenWeather;

namespace EntityFrameworkWeatherApp.Mediator.WeatherAPI;

public static class OpenWeatherResults
{
    public static async Task<Result<List<OpenWeatherGeoDirectModel>>> GetOpenWeatherGeoDirectResult(string location, ISender sender)
    {
        var query = new GetOpenWeatherGeoDirectQuery(location);

        var result = await sender.Send(query);
        return result;
    }

    public static async Task<Result<OpenWeatherGeoZipModel>> GetOpenWeatherGeoZipResult(string zip, ISender sender)
    {
        var query = new GetOpenWeatherGeoZipQuery(zip);

        var result = await sender.Send(query);
        return result;
    }

    public static async Task<Result<OpenWeatherDataModel>> GetOpenWeatherDirectLatLongResult(float latitude, float longitude, ISender sender)
    {
        var query = new GetOpenWeatherWeatherByLatLongQuery(latitude, longitude);

        var result = await sender.Send(query);
        return result;
    }

    public static async Task<Result<OpenWeatherDataModel>> GetOpenWeatherDirectLocationResult(string location, ISender sender)
    {
        var query = new GetOpenWeatherWeatherByLocationNameQuery(location);

        var result = await sender.Send(query);
        return result;
    }
}
