namespace EntityFrameworkWeatherApp.Domain.Entities.CosmosDB;

public abstract class BaseContainerEntity
{
    [JsonPropertyName("id")]
    public virtual required string Id { get; set; }
}
