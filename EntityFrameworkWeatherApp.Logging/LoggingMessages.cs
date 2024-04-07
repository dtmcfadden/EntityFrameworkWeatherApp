using System.Runtime.CompilerServices;

namespace EntityFrameworkWeatherApp.Logging;

public partial class LoggingMessages(ILogger logger)
{
    private readonly ILogger _logger = logger;

    /// <summary>
    /// Logs Azure Functions Class and Method name.
    /// </summary>
    /// <param name="ClassName">this.GetType().Name</param>
    /// <param name="CallerName">MethodBase.GetCurrentMethod().Name</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.AzureFunctionsHttpTriggerRequest,
        Level = LogLevel.Information,
        Message = "C# HTTP trigger Class Name:{ClassName} -> Request:{CallerName}")]
    public partial void AzureFunctionRequest(string ClassName, [CallerMemberName] string? CallerName = "");

    /// <summary>
    /// Logs Azure Function Class, Response, and Method name.
    /// </summary>
    /// <param name="ClassName">this.GetType().Name</param>
    /// <param name="ResponseType">resSvgDocument.GetType().Name</param>
    /// <param name="CallerName">[CallerMemberName] string? CallerName</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.AzureFunctionsHttpTriggerResponse,
        Level = LogLevel.Information,
        Message = "C# HTTP trigger Class Name:{ClassName} -> Response:{CallerName} -> Type:{ResponseType}")]
    public partial void AzureFunctionResponse(string ClassName, string ResponseType, [CallerMemberName] string? CallerName = "");

    /// <summary>
    /// Top level Azure Function exception handling.
    /// </summary>
    /// <param name="ex">Exception.</param>
    /// <param name="ClassName">this.GetType().Name</param>
    /// <param name="CallerName"></param>
    /// <remarks>Exception display is automatically handled.</remarks>
    [LoggerMessage(
        EventId = (int)LoggingEventId.AzureFunctionsHttpTriggerRequestException,
        Level = LogLevel.Error,
        Message = "Exception in Class Name:{ClassName} -> Method Name:{CallerName}")]
    public partial void AzureFunctionMethodException(Exception ex, string ClassName, [CallerMemberName] string? CallerName = "");

    /// <summary>
    /// Top level Azure Function error message.
    /// </summary>
    /// <param name="Msg"></param>
    /// <param name="ClassName"></param>
    /// <param name="CallerName"></param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.AzureFunctionsHttpTriggerRequestErrorMessage,
        Level = LogLevel.Error,
        Message = "Exception in Class Name:{ClassName} -> Method Name:{CallerName} Message:{Msg}")]
    public partial void AzureFunctionMethodErrorMessage(string ClassName, string Msg, [CallerMemberName] string? CallerName = "");

    /// <summary>
    /// Request Class Name.
    /// </summary>
    /// <param name="ClassRequest">typeof(TRequest).Name</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.LoggingBehaviorHandling,
        Level = LogLevel.Information,
        Message = "Handling Request:{ClassRequest}")]
    public partial void LoggingBehaviorHandling(string ClassRequest);

    /// <summary>
    /// Handled Request Class name and response Class name.
    /// </summary>
    /// <param name="ClassRequest">typeof(TRequest).Name</param>
    /// <param name="ClassResponse">typeof(TResponse).Name</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.LoggingBehaviorHandled,
        Level = LogLevel.Information,
        Message = "Handled Request:{ClassRequest} -> Response:{ClassResponse}")]
    public partial void LoggingBehaviorHandled(string ClassRequest, string ClassResponse);

    /// <summary>
    /// Request Class Name with parameters.
    /// </summary>
    /// <param name="ClassRequest">typeof(TRequest).Name</param>
    /// <param name="RequestParameterKeys"></param>
    /// <param name="RequestParameters"></param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.LoggingBehaviorHandlingWithParams,
        Level = LogLevel.Information,
        Message = "Handling Request:{ClassRequest} keys:{RequestParameterKeys} parameters:{RequestParameters}")]
    public partial void LoggingBehaviorHandlingWithParams(string ClassRequest, string RequestParameterKeys, string RequestParameters);

    /// <summary>
    /// Handled Request Class name and response Class name.
    /// </summary>
    /// <param name="ClassRequest">typeof(TRequest).Name</param>
    /// <param name="ClassResponse">typeof(TResponse).Name</param>
    /// <param name="Response">Serialized Response</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.LoggingBehaviorHandledWithResponse,
        Level = LogLevel.Information,
        Message = "Handled Request:{ClassRequest} -> ClassResponse:{ClassResponse} Response:{Response}")]
    public partial void LoggingBehaviorHandledWithResponse(string ClassRequest, string ClassResponse, string Response);

    /// <summary>
    /// Log empty variable that shouldn't be empty.
    /// </summary>
    /// <param name="ClassName">Class log is in.</param>
    /// <param name="EmptyVariable">Variable name that is empty</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.LogEmptyReturnVariable,
        Level = LogLevel.Information,
        Message = "ClassName:{ClassName} -> EmptyVariable:{EmptyVariable}")]
    public partial void LogEmptyReturnVariable(string ClassName, string EmptyVariable);

    /// <summary>
    /// Variable shouldn't be null. Was passed parameters.
    /// </summary>
    /// <param name="ClassName">Class that the log is in</param>
    /// <param name="ClassCall">Class that was called</param>
    /// <param name="ClassParams">Comma separated list of parameters</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.LogEmptyReturnVariableWithParameters,
        Level = LogLevel.Information,
        Message = "ClassName:{ClassName} -> ClassCall:{ClassCall} -> ClassParams:{ClassParams}")]
    public partial void LogEmptyReturnVariableWithParameters(string ClassName, string ClassCall, string ClassParams);

    /// <summary>
    /// Log failed database creation.
    /// </summary>
    /// <param name="DatabaseFailedList">Comma separated list of databases</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.DatabaseCreationFailed,
        Level = LogLevel.Critical,
        Message = "Database Creation Failed:{DatabaseFailedList}")]
    public partial void LogFailedDatabaseCreation(string DatabaseFailedList);

    /// <summary>
    /// Log failed container creation
    /// </summary>
    /// <param name="ContainerFailedList">Comma separated list of containers</param>
    [LoggerMessage(
        EventId = (int)LoggingEventId.ContainerCreationFailed,
        Level = LogLevel.Critical,
        Message = "Container Creation Failed:{ContainerFailedList}")]
    public partial void LogFailedContainerCreation(string ContainerFailedList);
}
