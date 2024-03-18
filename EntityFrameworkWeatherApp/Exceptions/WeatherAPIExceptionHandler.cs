using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherAPI.Exceptions.OpenWeather;
using WeatherAPI.Exceptions.WeatherAPI;

namespace EntityFrameworkWeatherApp.Exceptions;

internal sealed class WeatherAPIExceptionHandler(ILogger<WeatherAPIExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<WeatherAPIExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not OpenWeatherException and not WeatherAPIException)
        {
            return false;
        }

        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        _logger.LogError(
            exception,
            "Exception occurred: {Message}. TraceId: {TraceId}",
            exception.Message,
            traceId);

        var (exceptionStatusCode, exceptionTitle) = MapException(exception);

        var problemDetails = new ProblemDetails
        {
            Status = exceptionStatusCode,
            Title = exceptionTitle,
            Extensions = new Dictionary<string, object?>
            {
                {"traceId", traceId }
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static (int StatusCode, string Title) MapException(Exception exception)
    {
        return exception switch
        {
            OpenWeatherAPICallException => (StatusCodes.Status400BadRequest, "Error with OpenWeather"),
            OpenWeatherLogicException => (StatusCodes.Status400BadRequest, "Error with server logic for OpenWeather"),
            OpenWeatherEntryException => (StatusCodes.Status400BadRequest, exception.Message),
            OpenWeatherException => (StatusCodes.Status400BadRequest, "Error processing OpenWeather call"),
            WeatherAPIException => (StatusCodes.Status400BadRequest, "Error with WeatherAPI"),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };
    }
}
