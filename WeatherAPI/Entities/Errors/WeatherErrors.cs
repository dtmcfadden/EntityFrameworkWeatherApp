namespace WeatherAPI.Entities.Errors;
public static class WeatherErrors
{
    public static Error WeatherNoDataError(string CurrentFunction, string? LogMessage = default) =>
        new(CurrentFunction, "No Weather Data", LogMessage);
}
