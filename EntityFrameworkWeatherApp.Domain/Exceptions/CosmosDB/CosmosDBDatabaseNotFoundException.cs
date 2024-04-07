namespace EntityFrameworkWeatherApp.Domain.Exceptions.CosmosDB;
public class CosmosDBDatabaseNotFoundException : Exception
{
    public CosmosDBDatabaseNotFoundException() : base() { }
    public CosmosDBDatabaseNotFoundException(string message) : base(message) { }
    public CosmosDBDatabaseNotFoundException(string message, Exception inner) : base(message, inner) { }
}
