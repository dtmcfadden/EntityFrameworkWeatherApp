using MediatR;
using WeatherAPI.Entities.Enums;
using WeatherAPI.Entities.Errors;
using WeatherAPI.Entities.Interface;
using WeatherAPI.Mappers;
using WeatherAPI.Models.CombinedWeather;
using WeatherAPI.Requests.Queries.OpenWeather;
using WeatherAPI.Requests.Queries.WeatherAPI;

namespace WeatherAPI.Requests.Queries.CombinedWeather;
public record GetCombinedWeatherByLatLongQuery(float? Latitude, float? Longitude) :
    ICachedQuery<Result<CombinedWeatherDataModel>>
{
    public string CacheKey => $"combinedweather-latlon-{Latitude}{Longitude}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(1);
    public long? Size => 1;
}

public class GetCombinedWeatherByLatLongHandler(ISender sender, IWeatherCallCountEntity weatherCallCount) :
    IRequestHandler<GetCombinedWeatherByLatLongQuery, Result<CombinedWeatherDataModel>>
{
    private readonly ISender _sender = sender;
    private readonly IWeatherCallCountEntity _weatherCallCount = weatherCallCount;

    public async Task<Result<CombinedWeatherDataModel>> Handle(
        GetCombinedWeatherByLatLongQuery request,
        CancellationToken cancellationToken)
    {
        var latLong = new LatLongEntity(request.Latitude, request.Longitude);
        if (await latLong.IsValid(cancellationToken) == false)
            return new Result<CombinedWeatherDataModel>(
                LatLongEntityErrors.LatLongEntityValidationError(latLong.ToString()),
                await latLong.ValidationResult(cancellationToken));

        List<WeatherApiNamesEnums> weatherApiNames = _weatherCallCount.GetOrderedWeatherApi();

        while (weatherApiNames.Count > 0)
        {
            var combinedWeatherDataResult = weatherApiNames[0] switch
            {
                WeatherApiNamesEnums.OpenWeather => await GetOpenWeatherData(
                    request.Latitude, request.Longitude, cancellationToken),
                WeatherApiNamesEnums.WeatherApi => await GetWeatherApiData(
                    request.Latitude, request.Longitude, cancellationToken),
                _ => default
            };

            weatherApiNames.RemoveAt(0);

            if (combinedWeatherDataResult is not null &&
                (combinedWeatherDataResult.IsSuccess || weatherApiNames.Count == 0))
                return combinedWeatherDataResult;
        }

        return new Result<CombinedWeatherDataModel>(
            WeatherErrors.WeatherNoDataError("GetCombinedWeatherByLatLongHandler.Handle"));
    }

    private async Task<Result<CombinedWeatherDataModel>> GetOpenWeatherData(
        float? Latitude, float? Longitude, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetOpenWeatherWeatherByLatLongQuery(Latitude, Longitude),
            cancellationToken);
        if (result is null)
            return new Result<CombinedWeatherDataModel>();
        if (result.Value is null && result.Exception is not null)
            return new Result<CombinedWeatherDataModel>(
                result.GetException, result.GetError, result.GetValidationResult);
        if (result.Value is null)
            return new Result<CombinedWeatherDataModel>();

        var combinedWeatherDataModel = result.Value.AsCombinedWeatherData();
        if (combinedWeatherDataModel is null)
            return new Result<CombinedWeatherDataModel>(
                WeatherErrors.WeatherNoDataError("GetCombinedWeatherByLatLongHandler.GetOpenWeatherData"));

        return new Result<CombinedWeatherDataModel>(combinedWeatherDataModel);
    }

    private async Task<Result<CombinedWeatherDataModel>> GetWeatherApiData(
        float? Latitude, float? Longitude, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetWeatherAPIWeatherByLatLongQuery(Latitude, Longitude),
            cancellationToken);
        if (result is null)
            return new Result<CombinedWeatherDataModel>();
        if (result.Value is null && result.Exception is not null)
            return new Result<CombinedWeatherDataModel>(
                result.GetException, result.GetError, result.GetValidationResult);
        if (result.Value is null)
            return new Result<CombinedWeatherDataModel>();

        var combinedWeatherDataModel = result.Value.AsCombinedWeatherData();
        if (combinedWeatherDataModel is null)
            return new Result<CombinedWeatherDataModel>(
                WeatherErrors.WeatherNoDataError("GetCombinedWeatherByLatLongHandler.GetWeatherApiData"));

        return new Result<CombinedWeatherDataModel>(combinedWeatherDataModel);
    }
}