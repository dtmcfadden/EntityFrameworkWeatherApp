using MediatR;
using WeatherAPI.Entities.Errors;
using WeatherAPI.Models.WeatherAPI;

namespace WeatherAPI.Requests.Queries.WeatherAPI;
public record GetWeatherAPIWeatherByLatLongQuery(float? Latitude, float? Longitude) :
    ICachedQuery<Result<WeatherAPICurrentModel>>
{
    public string CacheKey => $"weatherapi-weather-latlon-{Latitude}{Longitude}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(1);
    public long? Size => 1;
}

public class GetWeatherAPIWeatherByLatLongHandler(IWeatherAPIHTTPService weatherAPIHTTPService) :
    IRequestHandler<GetWeatherAPIWeatherByLatLongQuery, Result<WeatherAPICurrentModel>>
{
    private readonly IWeatherAPIHTTPService _weatherAPIHTTPService = weatherAPIHTTPService;

    public async Task<Result<WeatherAPICurrentModel>> Handle(
        GetWeatherAPIWeatherByLatLongQuery request,
        CancellationToken cancellationToken)
    {
        var latLong = new LatLongEntity(request.Latitude, request.Longitude);
        if (await latLong.IsValid(cancellationToken) == false)
            return new Result<WeatherAPICurrentModel>(LatLongEntityErrors.LatLongEntityValidationError(latLong.ToString()), await latLong.ValidationResult(cancellationToken));

        return await _weatherAPIHTTPService.GetWeatherByLatLong(latLong, cancellationToken);
    }
}