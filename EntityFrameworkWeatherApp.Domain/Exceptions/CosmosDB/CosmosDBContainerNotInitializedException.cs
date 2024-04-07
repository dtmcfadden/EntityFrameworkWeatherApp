namespace EntityFrameworkWeatherApp.Domain.Exceptions.CosmosDB;
public class CosmosDBContainerNotInitializedException : Exception
{
    public CosmosDBContainerNotInitializedException() : base() { }
    public CosmosDBContainerNotInitializedException(string message) : base(message) { }
    public CosmosDBContainerNotInitializedException(string message, Exception inner) : base(message, inner) { }
}
