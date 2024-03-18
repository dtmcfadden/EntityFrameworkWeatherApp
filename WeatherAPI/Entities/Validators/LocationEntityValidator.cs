namespace WeatherAPI.Entities.Validators;
public class LocationEntityValidator : AbstractValidator<LocationEntity>
{
    public LocationEntityValidator()
    {
        RuleFor(loc => loc.Location)
            .Length(2, 100)
            .Matches(@"^[0-9a-zA-Z\s]+$").WithMessage("Please use only letters, numbers, or comma.");
    }
}
