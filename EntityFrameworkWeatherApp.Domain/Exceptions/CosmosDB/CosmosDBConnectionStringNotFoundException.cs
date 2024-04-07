namespace EntityFrameworkWeatherApp.Domain.Exceptions.CosmosDB;
public class CosmosDBConnectionStringNotFoundException : Exception
{
    public CosmosDBConnectionStringNotFoundException() : base() { }
    public CosmosDBConnectionStringNotFoundException(string message) : base(message) { }
    public CosmosDBConnectionStringNotFoundException(string message, Exception inner) : base(message, inner) { }
}
