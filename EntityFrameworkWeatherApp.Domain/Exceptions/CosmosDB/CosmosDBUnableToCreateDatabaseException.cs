namespace EntityFrameworkWeatherApp.Domain.Exceptions.CosmosDB;
public class CosmosDBUnableToCreateDatabaseException : Exception
{
    public CosmosDBUnableToCreateDatabaseException() : base() { }
    public CosmosDBUnableToCreateDatabaseException(string message) : base(message) { }
    public CosmosDBUnableToCreateDatabaseException(string message, Exception inner) : base(message, inner) { }
}
