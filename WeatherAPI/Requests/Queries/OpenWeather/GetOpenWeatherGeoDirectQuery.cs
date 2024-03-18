using MediatR;
using WeatherAPI.Errors;
using WeatherAPI.Models.OpenWeather;
using WeatherAPI.Services.Interface;

namespace WeatherAPI.Requests.Queries.OpenWeather;

public sealed record GetOpenWeatherGeoDirectQuery(string LocationQuery) :
    ICachedQuery<Result<List<OpenWeatherGeoDirectModel>?>?>
{
    public string CacheKey => $"openweather-geo-loc-{LocationQuery}";

    public TimeSpan? Expiration => TimeSpan.FromDays(10);
    public TimeSpan? SlidingExpiration => TimeSpan.FromDays(1);
    public long? Size => 1;
}

public sealed class GetOpenWeatherGeoDirectHandler(IOpenWeatherHTTPService openWeatherHTTPService) :
    IRequestHandler<GetOpenWeatherGeoDirectQuery, Result<List<OpenWeatherGeoDirectModel>?>?>
{
    private readonly IOpenWeatherHTTPService _openWeatherHTTPService = openWeatherHTTPService;

    public async Task<Result<List<OpenWeatherGeoDirectModel>?>?> Handle(
        GetOpenWeatherGeoDirectQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.LocationQuery))
            return new Result<List<OpenWeatherGeoDirectModel>?>(OpenWeatherErrors.LocationIsEmpty(request.LocationQuery));

        return await _openWeatherHTTPService.GetGeoDirect(request.LocationQuery, cancellationToken);
    }
}