using MediatR;
using WeatherAPI.Errors;
using WeatherAPI.Models.OpenWeather;

namespace WeatherAPI.Requests.Queries.OpenWeather;

public record GetOpenWeatherGeoZipQuery(string ZipQuery) :
    ICachedQuery<Result<OpenWeatherGeoZipModel>>
{
    public string CacheKey => $"openweather-geo-zip-{ZipQuery}";

    public TimeSpan? Expiration => TimeSpan.FromDays(10);
    public TimeSpan? SlidingExpiration => TimeSpan.FromDays(1);
    public long? Size => 1;
}

public class GetOpenWeatherGeoZipHandler(IOpenWeatherHTTPService openWeatherHTTPService) :
    IRequestHandler<GetOpenWeatherGeoZipQuery, Result<OpenWeatherGeoZipModel>>
{
    private readonly IOpenWeatherHTTPService _openWeatherHTTPService = openWeatherHTTPService;

    public async Task<Result<OpenWeatherGeoZipModel>> Handle(
        GetOpenWeatherGeoZipQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.ZipQuery))
            return new Result<OpenWeatherGeoZipModel>(OpenWeatherErrors.ZipIsEmpty(request.ZipQuery));

        return await _openWeatherHTTPService.GetGeoZip(request.ZipQuery, cancellationToken);
    }
}