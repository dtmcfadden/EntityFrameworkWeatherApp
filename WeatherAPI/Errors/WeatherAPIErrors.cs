namespace WeatherAPI.Errors;
public static class WeatherAPIErrors
{
    public static Error LatOrLongIsNull(string? LogMessage = null) =>
        new("WeatherAPI.LatOrLongIsNull",
            "Latitude or Longitude is null.",
            LogMessage);
    public static Error LatOrLongIsInvalid(string? LogMessage = null) =>
        new("WeatherAPI.LatOrLongIsInvalid",
            "Latitude should be between -90 and 90. Longitude should be between -180 and 180.",
            LogMessage);

    public static Error ErrorContactingWeatherAPI(string? LogMessage = null) =>
        new("WeatherAPI.ErrorContactingWeatherAPI",
            "Error contacting WeatherAPI",
            LogMessage);

    public static Error ErrorResponseFromWeatherAPI(string? LogMessage = null) =>
        new("WeatherAPI.ErrorResponseFromWeatherAPI",
            $"Error response from WeatherAPI. ({LogMessage})",
            LogMessage);
}
