﻿using Microsoft.Extensions.Options;
using WeatherAPI.Errors;
using WeatherAPI.Models.WeatherAPI;
using WeatherAPI.Services.Interface;

namespace WeatherAPI.Services;
public sealed class WeatherAPIHTTPService : IWeatherAPIHTTPService
{
    private readonly WeatherAPIOptions _weatherAPIOptions;
    private readonly HttpClient _client;
    private readonly string _weatherPath = "current.json?";
    private readonly string _apiKey;

    public WeatherAPIHTTPService(IOptions<WeatherAPIOptions> weatherAPIOptions, HttpClient client)
    {
        _weatherAPIOptions = weatherAPIOptions.Value;
        _apiKey = _weatherAPIOptions.APIKey;
        _client = client;
    }

    public async Task<Result<WeatherAPICurrentModel?>> GetWeatherByLatLong(LatLongEntity latLong,
        CancellationToken cancellationToken = default)
    {
        if (latLong.IsEmpty())
            return new Result<WeatherAPICurrentModel?>(WeatherAPIErrors.LatOrLongIsNull(latLong.ToString()));

        var url = $"{_weatherPath}key={_apiKey}&q={latLong.ToStringWithDecimals(" ")}";
        var response = await GetResultFromWeatherAPI<WeatherAPICurrentModel>(url, cancellationToken);

        if (response.IsSuccess
            && response?.Value?.Location != null
            && (((int)response?.Value?.Location?.Latitude != (int)latLong.Latitude) ||
                ((int)response?.Value?.Location?.Longitude != (int)latLong.Longitude)))
        {
            return new Result<WeatherAPICurrentModel?>(WeatherAPIErrors.LatOrLongIsInvalid(latLong.ToString()));
        }

        return response;
    }

    public async Task<Result<WeatherAPICurrentModel?>> GetWeatherByLocationName(LocationEntity locationName,
        CancellationToken cancellationToken = default)
    {
        var url = $"{_weatherPath}key={_apiKey}&q={locationName}";
        var response = await GetResultFromWeatherAPI<WeatherAPICurrentModel>(url, cancellationToken);

        return response;
    }

    private async Task<Result<T?>> GetResultFromWeatherAPI<T>(string url, CancellationToken cancellationToken = default)
    {
        // https://app.swaggerhub.com/apis-docs/WeatherAPI.com/WeatherAPI/1.0.2#/APIs/realtime-weather
        HttpResponseMessage response;

        try
        {
            response = await _client.GetAsync(url, cancellationToken);
        }
        catch (Exception ex)
        {
            return new Result<T?>(WeatherAPIErrors.ErrorContactingWeatherAPI(ex.Message));
        }

        T? content;
        if (response.IsSuccessStatusCode)
        {
            content = await response.Content.ReadFromJsonAsync<T>(cancellationToken);
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<WeatherAPIErrorModel>(cancellationToken);

            return new Result<T?>(WeatherAPIErrors.ErrorResponseFromWeatherAPI(string.Format("{0}:{1}", error?.Error.Code, error?.Error.Message)));
        }

        return content;
    }
}
