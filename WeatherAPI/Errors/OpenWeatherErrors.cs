namespace WeatherAPI.Errors;
public static class OpenWeatherErrors
{
    public static Error LocationIsEmpty(string? LogMessage = null) =>
        new("OpenWeather.LocationIsEmpty",
            "Location is empty",
            LogMessage);
    public static Error ZipIsEmpty(string? LogMessage = null) =>
        new("OpenWeather.ZipIsEmpty",
            "Zip/Postal Code is empty",
            LogMessage);
    public static Error LatOrLongIsNull(string? LogMessage = null) =>
        new("OpenWeather.LatOrLongIsNull",
            "Latitude or Longitude is null.",
            LogMessage);
    public static Error LatLongIsEmpty(string? LogMessage = null) =>
        new("OpenWeather.LatLongIsEmpty",
            "LatLong is empty",
            LogMessage);
    public static Error LatLongIsInvalid(string? LogMessage = null) =>
        new("OpenWeather.LatLongIsInvalid",
            "LatLong is invalid",
            LogMessage);
    public static Error GeoDirectNotValidLocation(string? LogMessage = null) =>
        new("OpenWeather.GeoDirectNotValidLocation",
            "Not valid location entry. Use {city name},{state code},{country code}. City is optional.",
            LogMessage);
    public static Error GeoZipNotValidZip(string? LogMessage = null) =>
        new("OpenWeather.GeoZipNotValidZip",
            "Not valid zip/postal code entry. Use {zip code},{country code}. Country is optional.",
            LogMessage);
    public static Error ErrorContactingOpenWeather(string? LogMessage = null) =>
        new("OpenWeather.ErrorContactingOpenWeather",
            "Error contacting OpenWeather",
            LogMessage);
    public static Error ErrorResponseFromOpenWeather(string? LogMessage = null) =>
        new("OpenWeather.ErrorResponseFromOpenWeather",
            "Error response from OpenWeather.",
            LogMessage);

    public static Error APIKeyIsMissing(string? LogMessage = null) =>
        new("OpenWeather.APIKeyIsMissing",
            "API Key is missing. Please provide.",
            LogMessage);
}
