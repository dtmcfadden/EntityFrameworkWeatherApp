using EntityFrameworkWeatherApp.Mediator.WeatherAPI;

namespace EntityFrameworkWeatherApp.Mediator.WeatherAPIRequests;

// https://localhost:7109/api/OpenWeather/geodirect?locationQuery=London

internal static class OpenWeatherRequests
{
    public static async Task<IResult> GetOpenWeatherGeoDirect(string location, ISender sender)
    {
        var result = await OpenWeatherResults.GetOpenWeatherGeoDirectResult(location, sender);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }

    public static async Task<IResult> GetOpenWeatherGeoZip(string zip, ISender sender)
    {
        var result = await OpenWeatherResults.GetOpenWeatherGeoZipResult(zip, sender);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }

    public static async Task<IResult> GetOpenWeatherDirectLatLong(float latitude, float longitude, ISender sender)
    {
        var result = await OpenWeatherResults.GetOpenWeatherDirectLatLongResult(latitude, longitude, sender);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }

    public static async Task<IResult> GetOpenWeatherDirectLocation(string location, ISender sender)
    {
        var result = await OpenWeatherResults.GetOpenWeatherDirectLocationResult(location, sender);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }
}
