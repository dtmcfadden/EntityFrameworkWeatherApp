using Microsoft.Extensions.Options;
using WeatherAPI.Errors;
using WeatherAPI.Models.WeatherAPI;

namespace WeatherAPI.Services;
public sealed class WeatherAPIHTTPService : IWeatherAPIHTTPService
{
    private readonly EnvironmentOptions _environmentOptions;
    private readonly HttpClient _client;
    private readonly string _weatherPath = "current.json?";
    private readonly string _apiKey;
    private readonly Error? _isImplemented;

    public WeatherAPIHTTPService(
        IOptions<EnvironmentOptions> environmentOptions,
        HttpClient client)
    {
        _environmentOptions = environmentOptions.Value;
        _apiKey = _environmentOptions.WeatherAPIApiKey;
        _client = client;

        if (string.IsNullOrEmpty(_apiKey) == true)
            _isImplemented = WeatherAPIErrors.APIKeyIsMissing();
    }

    public async Task<Result<WeatherAPICurrentModel>> GetWeatherByLatLong(LatLongEntity latLong,
        CancellationToken cancellationToken = default)
    {
        if (_isImplemented != null)
            return new Result<WeatherAPICurrentModel>(_isImplemented);

        if (latLong.IsEmpty())
            return new Result<WeatherAPICurrentModel>(WeatherAPIErrors.LatOrLongIsNull(latLong.ToString()));

        var url = $"{_weatherPath}key={_apiKey}&q={latLong.ToStringWithDecimals(" ")}";
        var response = await GetResultFromWeatherAPI<WeatherAPICurrentModel>(url, cancellationToken);

        if (response.IsSuccess
            && response.Value?.Location != null
            && response.Value.Location?.Latitude != null && response.Value.Location?.Longitude != null
            && latLong.Latitude != null && latLong.Longitude != null
            && (((int)response.Value.Location.Latitude != (int)latLong.Latitude) ||
                ((int)response.Value.Location.Longitude != (int)latLong.Longitude)))
        {
            return new Result<WeatherAPICurrentModel>(WeatherAPIErrors.LatOrLongIsInvalid(latLong.ToString()));
        }

        return response;
    }

    public async Task<Result<WeatherAPICurrentModel>> GetWeatherByLocationName(LocationEntity locationName,
        CancellationToken cancellationToken = default)
    {
        if (_isImplemented != null)
            return new Result<WeatherAPICurrentModel>(_isImplemented);

        var url = $"{_weatherPath}key={_apiKey}&q={locationName}";
        var response = await GetResultFromWeatherAPI<WeatherAPICurrentModel>(url, cancellationToken);

        return response;
    }

    private async Task<Result<T>> GetResultFromWeatherAPI<T>(string url, CancellationToken cancellationToken = default)
    {
        // https://app.swaggerhub.com/apis-docs/WeatherAPI.com/WeatherAPI/1.0.2#/APIs/realtime-weather

        if (_isImplemented != null)
            return new Result<T>(_isImplemented);

        HttpResponseMessage response;

        try
        {
            response = await _client.GetAsync(url, cancellationToken);
        }
        catch (Exception ex)
        {
            return new Result<T>(WeatherAPIErrors.ErrorContactingWeatherAPI(ex.Message));
        }

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<T>(cancellationToken);
            if (jsonResponse == null)
                return new Result<T>(WeatherAPIErrors.ErrorResponseFromWeatherAPI("Response is null."));

            return jsonResponse;
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<WeatherAPIErrorModel>(cancellationToken);

            return new Result<T>(WeatherAPIErrors.ErrorResponseFromWeatherAPI(string.Format("{0}:{1}", error?.Error.Code, error?.Error.Message)));
        }
    }
}
