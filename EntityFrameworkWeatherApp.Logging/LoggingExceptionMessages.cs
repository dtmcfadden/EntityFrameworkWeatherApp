namespace EntityFrameworkWeatherApp.Logging;

public partial class LoggingExceptionMessages(ILogger logger)
{
    private readonly ILogger _logger = logger;

    [LoggerMessage(
        EventId = (int)LoggingEventId.ExceptionHttpStatusCode100s,
        Level = LogLevel.Warning,
        Message = "StatusCode:{StatusCode}:Informational responses ExceptionName: {ExceptionName} Message: {Message}")]
    public partial void LogExceptionHttpStatusCode100s(Exception ex, int StatusCode, string ExceptionName, string Message);

    [LoggerMessage(
        EventId = (int)LoggingEventId.ExceptionHttpStatusCode200s,
        Level = LogLevel.Warning,
        Message = "StatusCode:{StatusCode}:Successful responses ExceptionName: {ExceptionName} Message: {Message}")]
    public partial void LogExceptionHttpStatusCode200s(Exception ex, int StatusCode, string ExceptionName, string Message);

    [LoggerMessage(
        EventId = (int)LoggingEventId.ExceptionHttpStatusCode300s,
        Level = LogLevel.Warning,
        Message = "StatusCode:{StatusCode}:Redirection messages ExceptionName: {ExceptionName} Message: {Message}")]
    public partial void LogExceptionHttpStatusCode300s(Exception ex, int StatusCode, string ExceptionName, string Message);

    [LoggerMessage(
        EventId = (int)LoggingEventId.ExceptionHttpStatusCode400s,
        Level = LogLevel.Warning,
        Message = "StatusCode:{StatusCode}:Client error responses ExceptionName: {ExceptionName} Message: {Message}")]
    public partial void LogExceptionHttpStatusCode400s(Exception ex, int StatusCode, string ExceptionName, string Message);

    [LoggerMessage(
        EventId = (int)LoggingEventId.ExceptionHttpStatusCode500s,
        Level = LogLevel.Error,
        Message = "StatusCode:{StatusCode}:Server error responses ExceptionName: {ExceptionName} Message: {Message}")]
    public partial void LogExceptionHttpStatusCode500s(Exception ex, int StatusCode, string ExceptionName, string Message);
}
