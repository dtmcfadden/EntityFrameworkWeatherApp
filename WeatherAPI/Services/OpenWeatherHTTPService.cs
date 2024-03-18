using Microsoft.Extensions.Options;
using WeatherAPI.Errors;
using WeatherAPI.Models.OpenWeather;

namespace WeatherAPI.Services;

/// <summary>
/// API Service for OpenWeatherMap https://openweathermap.org/
/// </summary>
public sealed class OpenWeatherHTTPService : IOpenWeatherHTTPService
{
    private readonly OpenWeatherOptions _openWeatherOptions;
    private readonly HttpClient _client;
    private readonly string _dataWeatherVer = "data/2.5/weather?";
    private readonly string _geoDirectVer = "geo/1.0/direct?";
    private readonly string _geoZipVer = "geo/1.0/zip?";

    private readonly string _apiKey;

    public OpenWeatherHTTPService(
        IOptions<OpenWeatherOptions> openWeatherOptions,
        HttpClient client)
    {
        _openWeatherOptions = openWeatherOptions.Value;
        _apiKey = _openWeatherOptions.APIKey;
        _client = client;
    }

    public async Task<Result<OpenWeatherDataModel?>> GetWeatherByLatLong(LatLongEntity latLong,
        CancellationToken cancellationToken = default)
    {
        if (latLong.IsEmpty())
            return new Result<OpenWeatherDataModel?>(OpenWeatherErrors.LatOrLongIsNull(latLong.ToString()));

        var url = $"{_dataWeatherVer}lat={latLong.Latitude}&lon={latLong.Longitude}&appid={_apiKey}";
        var response = await GetResultFromOpenWeather<OpenWeatherDataModel?>(url, cancellationToken);

        return response;
    }

    public async Task<Result<List<OpenWeatherGeoDirectModel?>?>?> GetGeoDirect(string LocationQuery,
        CancellationToken cancellationToken = default)
    {
        var url = $"{_geoDirectVer}q={LocationQuery}&limit=1&appid={_apiKey}";
        var response = await GetResultFromOpenWeather<List<OpenWeatherGeoDirectModel?>?>(url, cancellationToken);

        if (response.IsFailure || response?.Value?.Count == 0)
            return new Result<List<OpenWeatherGeoDirectModel?>?>(OpenWeatherErrors.GeoDirectNotValidLocation(LocationQuery));

        return response;
    }

    public async Task<Result<OpenWeatherGeoZipModel?>> GetGeoZip(string ZipQuery,
        CancellationToken cancellationToken = default)
    {
        var url = $"{_geoZipVer}zip={ZipQuery}&appid={_apiKey}";
        var response = await GetResultFromOpenWeather<OpenWeatherGeoZipModel?>(url, cancellationToken);

        if (response == null)
            return new Result<OpenWeatherGeoZipModel?>(OpenWeatherErrors.GeoZipNotValidZip(ZipQuery));

        return response;
    }

    private async Task<Result<T?>> GetResultFromOpenWeather<T>(string url, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response;

        try
        {
            response = await _client.GetAsync(url, cancellationToken);
        }
        catch (Exception ex)
        {
            return new Result<T?>(OpenWeatherErrors.ErrorContactingOpenWeather(ex.Message));
        }

        T? content;
        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadFromJsonAsync<T>(cancellationToken);
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<OpenWeatherErrorModel>(cancellationToken);

            return new Result<T?>(OpenWeatherErrors.ErrorResponseFromOpenWeather(string.Format("{0}:{1}", error?.Code, error?.Message)));
        }

        return content;
    }
}
