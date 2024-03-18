using MediatR;
using WeatherAPI.Entities.Errors;
using WeatherAPI.Models.OpenWeather;

namespace WeatherAPI.Requests.Queries.OpenWeather;
public record GetOpenWeatherWeatherByLatLongQuery(float? Latitude, float? Longitude) :
    ICachedQuery<Result<OpenWeatherDataModel?>>
{
    public string CacheKey => $"openweather-direct-latlon-{Latitude}{Longitude}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(1);
    public long? Size => 1;
}

public class GetOpenWeatherWeatherByLatLongHandler(IOpenWeatherHTTPService openWeatherHTTPService) :
    IRequestHandler<GetOpenWeatherWeatherByLatLongQuery, Result<OpenWeatherDataModel?>>
{
    private readonly IOpenWeatherHTTPService _openWeatherHTTPService = openWeatherHTTPService;
    //private readonly LatLongEntityValidator _validator = validator;

    public async Task<Result<OpenWeatherDataModel?>> Handle(
        GetOpenWeatherWeatherByLatLongQuery request,
        CancellationToken cancellationToken)
    {
        var latLong = new LatLongEntity(request.Latitude, request.Longitude);
        //var validationResult = await _validator.ValidateAsync(latLong, cancellationToken);
        if (await latLong.IsValid(cancellationToken) == false)
            return new Result<OpenWeatherDataModel?>(LatLongEntityErrors.LatLongEntityValidationError(latLong.ToString()), await latLong.ValidationResult(cancellationToken));

        return await _openWeatherHTTPService.GetWeatherByLatLong(latLong, cancellationToken);
    }
}
