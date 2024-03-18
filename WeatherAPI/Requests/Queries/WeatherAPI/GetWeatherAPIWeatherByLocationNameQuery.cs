using MediatR;
using WeatherAPI.Entities.Errors;
using WeatherAPI.Models.WeatherAPI;

namespace WeatherAPI.Requests.Queries.WeatherAPI;
public record GetWeatherAPIWeatherByLocationNameQuery(string LocationName) :
    ICachedQuery<Result<WeatherAPICurrentModel?>>
{
    public string CacheKey => $"weatherapi-weather-location-{LocationName}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(1);
    public long? Size => 1;
}

public class GetWeatherAPIWeatherByLocationNameHandler(IWeatherAPIHTTPService weatherAPIHTTPService) :
    IRequestHandler<GetWeatherAPIWeatherByLocationNameQuery, Result<WeatherAPICurrentModel?>>
{
    private readonly IWeatherAPIHTTPService _weatherAPIHTTPService = weatherAPIHTTPService;

    public async Task<Result<WeatherAPICurrentModel?>> Handle(
        GetWeatherAPIWeatherByLocationNameQuery request,
        CancellationToken cancellationToken)
    {
        var location = new LocationEntity(request.LocationName);
        if (await location.IsValid(cancellationToken) == false)
            return new Result<WeatherAPICurrentModel?>(LocationEntityErrors.LocationEntityValidationError(location.ToString()), await location.ValidationResult(cancellationToken));

        return await _weatherAPIHTTPService.GetWeatherByLocationName(location, cancellationToken);
    }
}