namespace EntityFrameworkWeatherApp.Mediator.WeatherAPI.WeatherAPI;

public class WeatherAPIRequests
{
    public static async Task<IResult> GetWeatherAPIWeatherLatLong(
        float latitude, float longitude,
        ISender sender, HttpContext? context)
    {
        var result = await WeatherAPIResults.GetWeatherAPIWeatherByLatLongResult(
            latitude, longitude,
            sender, new() { Context = context });

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }

    public static async Task<IResult> GetWeatherAPIWeatherByLocationName(
        string location,
        ISender sender, HttpContext? context)
    {
        var result = await WeatherAPIResults.GetWeatherAPIWeatherByLocationNameResult(
            location,
            sender, new() { Context = context });

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }
}
