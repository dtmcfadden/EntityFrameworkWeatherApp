

namespace EntityFrameworkWeatherApp.Logging;

public partial class LoggingCosmosExceptionMessages(ILogger logger)
{
    private readonly ILogger _logger = logger;

    [LoggerMessage(
        EventId = (int)LoggingEventId.CosmosAddItemAsync,
        Level = LogLevel.Error,
        Message = "CosmosAddItemAsync Item:{item}")]
    public partial void LogCosmosExceptionAddItemAsync(Exception ex, string item);
}
