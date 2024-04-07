public enum LoggingEventId
{
    /// <summary>
    /// Azure Functions Http Trigger Request.
    /// </summary>
    AzureFunctionsHttpTriggerRequest = 1,
    AzureFunctionsHttpTriggerResponse = 2,
    AzureFunctionsHttpTriggerRequestException = 5,
    AzureFunctionsHttpTriggerRequestErrorMessage = 9,
    LoggingBehaviorHandling = 20,
    LoggingBehaviorHandled = 21,
    LoggingBehaviorHandlingWithParams = 23,
    LoggingBehaviorHandledWithResponse = 27,
    LogEmptyReturnVariable = 50,
    LogEmptyReturnVariableWithParameters = 51,
    DatabaseCreationFailed = 60,
    ContainerCreationFailed = 61,
    ExceptionHttpStatusCode100s = 101,
    ExceptionHttpStatusCode200s = 102,
    ExceptionHttpStatusCode300s = 103,
    ExceptionHttpStatusCode400s = 104,
    ExceptionHttpStatusCode500s = 105,

    CosmosAddItemAsync = 200,
    CosmosAddItemsAsync = 201,

    CosmosDeleteItemAsync = 210,
    CosmosDeleteContainer = 211,

    CosmosGetItemAsync = 220,
    CosmosGetItemsByIdAndPartitionKeyAsync = 221,
    CosmosGetItemsAsync = 222,

    CosmosUpdateItemAsync = 230,

    CosmosPatchItemAsync = 240,
    CosmosPatchOrCreateSingleItemAsync = 241,

    CosmosGenerateContainer = 250,
    CosmosGenerateContainerWithReturn = 251,

    CosmosGetIdsByIds = 260,

    CosmosGetContainerSettingsInfo = 270,
    CosmosGetDatabaseSettingsInfo = 271
}