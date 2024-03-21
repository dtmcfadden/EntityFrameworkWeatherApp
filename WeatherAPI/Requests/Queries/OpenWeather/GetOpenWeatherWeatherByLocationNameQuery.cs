
using MediatR;
using WeatherAPI.Common;
using WeatherAPI.Errors;
using WeatherAPI.Models.OpenWeather;

namespace WeatherAPI.Requests.Queries.OpenWeather;
public record GetOpenWeatherWeatherByLocationNameQuery(string LocationName) :
    ICachedQuery<Result<OpenWeatherDataModel>>
{
    public string CacheKey => $"openweather-direct-location-{LocationName}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(1);
    public long? Size => 1;
}

public class GetOpenWeatherWeatherByLocationNameHandler(
    ISender sender,
    IOpenWeatherHTTPService openWeatherHTTPService,
    LocationStringMatches locationStringMatches) :
    IRequestHandler<GetOpenWeatherWeatherByLocationNameQuery, Result<OpenWeatherDataModel>>
{
    private readonly IOpenWeatherHTTPService _openWeatherHTTPService = openWeatherHTTPService;
    private readonly LocationStringMatches _locationStringMatches = locationStringMatches;
    private readonly ISender _sender = sender;

    public async Task<Result<OpenWeatherDataModel>> Handle(
        GetOpenWeatherWeatherByLocationNameQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.LocationName))
            return new Result<OpenWeatherDataModel>(OpenWeatherErrors.LocationIsEmpty());

        var locationType = _locationStringMatches.GetLocationTypeFromString(request.LocationName);
        var returnLatLong = new LatLongEntity();
        Error? returnError = null;
        (LatLongEntity LatLong, Error? Error) geoData = (returnLatLong, default);

        if (locationType.IsAddress)
        {
            geoData = await GetOpenWeatherGeoDirectLatLong(request.LocationName, cancellationToken, _sender);
            if (geoData.Error != null)
                returnError = geoData.Error;
            if (geoData.LatLong.IsEmpty())
                geoData = await GetOpenWeatherGeoZipLatLong(request.LocationName, cancellationToken, _sender);
        }
        else
        {
            geoData = await GetOpenWeatherGeoZipLatLong(request.LocationName, cancellationToken, _sender);
            if (geoData.Error != null)
                returnError = geoData.Error;
            if (geoData.LatLong.IsEmpty())
                geoData = await GetOpenWeatherGeoDirectLatLong(request.LocationName, cancellationToken, _sender);
        }

        if (geoData.LatLong.IsEmpty())
        {
            if (returnError != null)
                return new Result<OpenWeatherDataModel>(returnError);
            return new Result<OpenWeatherDataModel>(OpenWeatherErrors.LatLongIsEmpty());
        }

        return await _openWeatherHTTPService.GetWeatherByLatLong(geoData.LatLong, cancellationToken);
    }

    private async Task<(LatLongEntity LatLong, Error? Error)> GetOpenWeatherGeoDirectLatLong(string locationName, CancellationToken cancellationToken, ISender sender)
    {
        var geoDirectResult = await sender.Send(new GetOpenWeatherGeoDirectQuery(locationName), cancellationToken);
        var latLong = new LatLongEntity();

        if (geoDirectResult != null && geoDirectResult.IsSuccess && geoDirectResult?.Value?.Count > 0)
        {
            latLong = new LatLongEntity(
                geoDirectResult.Value[0].Latitude,
                geoDirectResult.Value[0].Longitude);
            //latLong.Latitude = geoDirectResult.Value[0].Latitude;
            //latLong.Longitude = geoDirectResult.Value[0].Longitude;
        }
        return (latLong, geoDirectResult?.GetError);
    }

    private async Task<(LatLongEntity LatLong, Error? Error)> GetOpenWeatherGeoZipLatLong(string locationName, CancellationToken cancellationToken, ISender sender)
    {
        var geoZipResult = await sender.Send(new GetOpenWeatherGeoZipQuery(locationName), cancellationToken);
        var latLong = new LatLongEntity();

        if (geoZipResult.IsSuccess)
        {
            latLong = new LatLongEntity(
                geoZipResult?.Value?.Latitude,
                geoZipResult?.Value?.Longitude);
        }
        return (latLong, geoZipResult?.GetError);
    }
}