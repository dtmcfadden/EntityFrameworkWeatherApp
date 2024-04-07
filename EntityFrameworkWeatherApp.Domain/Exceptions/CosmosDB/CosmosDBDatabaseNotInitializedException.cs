namespace EntityFrameworkWeatherApp.Domain.Exceptions.CosmosDB;
public class CosmosDBDatabaseNotInitializedException : Exception
{
    public CosmosDBDatabaseNotInitializedException() : base() { }
    public CosmosDBDatabaseNotInitializedException(string message) : base(message) { }
    public CosmosDBDatabaseNotInitializedException(string message, Exception inner) : base(message, inner) { }
}
