namespace EntityFrameworkWeatherApp.Domain.Exceptions.CosmosDB;
public class CosmosDBContainerNotFoundException : Exception
{
    public CosmosDBContainerNotFoundException() : base() { }
    public CosmosDBContainerNotFoundException(string message) : base(message) { }
    public CosmosDBContainerNotFoundException(string message, Exception inner) : base(message, inner) { }
}
