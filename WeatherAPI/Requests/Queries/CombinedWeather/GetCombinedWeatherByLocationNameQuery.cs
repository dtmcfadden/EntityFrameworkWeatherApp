using MediatR;
using WeatherAPI.Entities.Enums;
using WeatherAPI.Entities.Errors;
using WeatherAPI.Entities.Interface;
using WeatherAPI.Mappers;
using WeatherAPI.Models.CombinedWeather;
using WeatherAPI.Requests.Queries.OpenWeather;
using WeatherAPI.Requests.Queries.WeatherAPI;

namespace WeatherAPI.Requests.Queries.CombinedWeather;
public record GetCombinedWeatherByLocationNameQuery(string LocationName) :
    ICachedQuery<Result<CombinedWeatherDataModel>>
{
    public string CacheKey => $"combinedweather-location-{LocationName}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(15);
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(5);
    public long? Size => 1;
}

public class GetCombinedWeatherByLocationNameHandler(ISender sender, IWeatherCallCountEntity weatherCallCount) :
    IRequestHandler<GetCombinedWeatherByLocationNameQuery, Result<CombinedWeatherDataModel>>
{
    private readonly ISender _sender = sender;
    private readonly IWeatherCallCountEntity _weatherCallCount = weatherCallCount;

    public async Task<Result<CombinedWeatherDataModel>> Handle(
        GetCombinedWeatherByLocationNameQuery request,
        CancellationToken cancellationToken)
    {
        var location = new LocationEntity(request.LocationName);
        if (await location.IsValid(cancellationToken) == false)
            return new Result<CombinedWeatherDataModel>(
                LocationEntityErrors.LocationEntityValidationError(location.ToString()),
                await location.ValidationResult(cancellationToken));

        List<WeatherApiNamesEnums> weatherApiNames = _weatherCallCount.GetOrderedWeatherApi();

        while (weatherApiNames.Count > 0)
        {
            var combinedWeatherDataResult = weatherApiNames[0] switch
            {
                WeatherApiNamesEnums.OpenWeather => await GetOpenWeatherData(
                    request.LocationName, cancellationToken),
                WeatherApiNamesEnums.WeatherApi => await GetWeatherApiData(
                    request.LocationName, cancellationToken),
                _ => default
            };

            weatherApiNames.RemoveAt(0);

            if (combinedWeatherDataResult is not null &&
                (combinedWeatherDataResult.IsSuccess || weatherApiNames.Count == 0))
                return combinedWeatherDataResult;
        }

        return new Result<CombinedWeatherDataModel>(
            WeatherErrors.WeatherNoDataError("GetCombinedWeatherByLocationNameHandler.Handle"));
    }

    private async Task<Result<CombinedWeatherDataModel>> GetOpenWeatherData(
        string LocationName, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetOpenWeatherWeatherByLocationNameQuery(LocationName),
            cancellationToken);
        if (result is null)
            return new Result<CombinedWeatherDataModel>();
        if (result.Value is null && result.Exception?.Count > 0)
            return new Result<CombinedWeatherDataModel>(
                result.GetException, result.GetError, result.GetValidationResult);
        if (result.Value is null)
            return new Result<CombinedWeatherDataModel>();

        var combinedWeatherDataModel = result.Value.AsCombinedWeatherData();
        if (combinedWeatherDataModel is null)
            return new Result<CombinedWeatherDataModel>(
                WeatherErrors.WeatherNoDataError("GetCombinedWeatherByLocationNameHandler.GetOpenWeatherData"));

        return new Result<CombinedWeatherDataModel>(combinedWeatherDataModel);
    }

    private async Task<Result<CombinedWeatherDataModel>> GetWeatherApiData(
        string LocationName, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetWeatherAPIWeatherByLocationNameQuery(LocationName),
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
                WeatherErrors.WeatherNoDataError("GetCombinedWeatherByLocationNameHandler.GetWeatherApiData"));

        return new Result<CombinedWeatherDataModel>(combinedWeatherDataModel);
    }
}