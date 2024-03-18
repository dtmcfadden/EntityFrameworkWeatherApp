using FluentValidation.Results;
using System.Text;
using WeatherAPI.Entities.Validators;

namespace WeatherAPI.Entities;
public class LatLongEntity(float? latitude = null, float? longitude = null)
{
    public float? Latitude { get; init; } = latitude;
    public float? Longitude { get; init; } = longitude;

    private ValidationResult? _validationResult = null;

    public bool IsEmpty()
    {
        return Latitude == null || Longitude == null;
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
            var validator = new LatLongEntityValidator();
            _validationResult = await validator.ValidateAsync(this, cancellationToken);
        }

        return _validationResult;
    }

    public override string ToString()
    {
        return Latitude + "," + Longitude;
    }
    public string ToString(string divider)
    {
        return Latitude + divider + Longitude;
    }

    public string ToStringWithDecimals(string divider)
    {
        StringBuilder returnString = new();

        if (Latitude is not null)
        {
            float tLat = (float)Latitude;
            returnString.Append(tLat.ToString("0.0000"));
        }

        returnString.Append(divider);

        if (Longitude is not null)
        {
            float tLong = (float)Longitude;
            returnString.Append(tLong.ToString("0.0000"));
        }

        return returnString.ToString();
    }
}
