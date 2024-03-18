using FluentValidation.Results;
using WeatherAPI.Entities.Validators;

namespace WeatherAPI.Entities;
public class LocationEntity(string location)
{
    public string? Location { get; init; } = location;

    private ValidationResult? _validationResult = null;

    public bool IsEmpty()
    {
        return Location == null;
    }

    public async Task<bool> IsValid(CancellationToken cancellationToken = default)
    {
        _validationResult ??= await ValidationResult(cancellationToken);

        return _validationResult.IsValid;
    }

    public async Task<ValidationResult> ValidationResult(CancellationToken cancellationToken = default)
    {
        if (_validationResult == null)
        {
            var validator = new LocationEntityValidator();
            _validationResult = await validator.ValidateAsync(this, cancellationToken);
        }

        return _validationResult;
    }

    public override string ToString()
    {
        return Location ?? "";
    }
}
