using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EntityFrameworkWeatherApp.Exceptions;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
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
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };
    }
}
