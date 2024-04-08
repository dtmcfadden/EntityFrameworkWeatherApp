using MediatR;
using WeatherAPI.Entities.Errors;
using WeatherAPI.Models.OpenWeather;

namespace WeatherAPI.Requests.Queries.OpenWeather;

[Time]
public record GetOpenWeatherWeatherByLatLongQuery(float? Latitude, float? Longitude) :
    ICachedQuery<Result<OpenWeatherDataModel>>
{
    public string CacheKey => $"openweather-direct-latlon-{Latitude}{Longitude}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(15);
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(5);
    public long? Size => 1;
}

[Time]
public class GetOpenWeatherWeatherByLatLongHandler(IOpenWeatherHTTPService openWeatherHTTPService) :
    IRequestHandler<GetOpenWeatherWeatherByLatLongQuery, Result<OpenWeatherDataModel>>
{
    private readonly IOpenWeatherHTTPService _openWeatherHTTPService = openWeatherHTTPService;

    public async Task<Result<OpenWeatherDataModel>> Handle(
        GetOpenWeatherWeatherByLatLongQuery request,
        CancellationToken cancellationToken)
    {
        var latLong = new LatLongEntity(request.Latitude, request.Longitude);
        if (await latLong.IsValid(cancellationToken) == false)
            return new Result<OpenWeatherDataModel>(LatLongEntityErrors.LatLongEntityValidationError(latLong.ToString()), await latLong.ValidationResult(cancellationToken));

        return await _openWeatherHTTPService.GetWeatherByLatLong(latLong, cancellationToken);
    }
}
