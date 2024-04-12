namespace EntityFrameworkWeatherApp.Mediator.WeatherAPI.CombinedWeather;

internal static class CombinedWeatherRequests
{
    public static async Task<IResult> GetCombinedWeatherLatLong(
        float latitude, float longitude,
        ISender sender, HttpContext? context)
    {
        var result = await CombinedWeatherResults.GetCombinedWeatherLatLongResult(
            latitude, longitude,
            sender, new() { Context = context });

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }

    public static async Task<IResult> GetCombinedWeatherLocation(
        string location,
        ISender sender, HttpContext? context)
    {
        var result = await CombinedWeatherResults.GetCombinedWeatherLocationResult(
            location,
            sender, new() { Context = context });

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }
}
