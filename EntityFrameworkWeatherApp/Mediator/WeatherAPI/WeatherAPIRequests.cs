namespace EntityFrameworkWeatherApp.Mediator.WeatherAPI;

public class WeatherAPIRequests
{
    public static async Task<IResult> GetWeatherAPIWeatherLatLong(float latitude, float longitude, ISender sender)
    {
        var result = await WeatherAPIResults.GetWeatherAPIWeatherByLatLongResult(latitude, longitude, sender);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }

    public static async Task<IResult> GetWeatherAPIWeatherByLocationName(string location, ISender sender)
    {
        var result = await WeatherAPIResults.GetWeatherAPIWeatherByLocationNameResult(location, sender);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Exception);
    }
}
