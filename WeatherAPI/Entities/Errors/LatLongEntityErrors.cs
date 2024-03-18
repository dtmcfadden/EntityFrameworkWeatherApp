namespace WeatherAPI.Entities.Errors;
public static class LatLongEntityErrors
{
    public static Error LatLongEntityValidationError(string? LogMessage = default) =>
        new("LatLongEntity.LatLongEntityValidationError", "Validation Error", LogMessage);
}
