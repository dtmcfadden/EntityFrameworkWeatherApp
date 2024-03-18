namespace WeatherAPI.Entities.Validators;
public class LatLongEntityValidator : AbstractValidator<LatLongEntity>
{
    public LatLongEntityValidator()
    {
        RuleFor(ll => ll.Latitude)
            .NotEmpty()
            .InclusiveBetween(-90, 90);

        RuleFor(ll => ll.Longitude)
            .NotEmpty()
            .InclusiveBetween(-180, 180);

    }
}
