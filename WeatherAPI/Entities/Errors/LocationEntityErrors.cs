namespace WeatherAPI.Entities.Errors;
public static class LocationEntityErrors
{
    public static Error LocationEntityValidationError(string? LogMessage = default) =>
        new("LocationEntity.LocationEntityValidationError", "Validation Error", LogMessage);
}
