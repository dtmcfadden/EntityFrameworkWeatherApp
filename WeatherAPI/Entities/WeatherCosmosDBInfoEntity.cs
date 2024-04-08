using EntityFrameworkWeatherApp.Domain.Entities.CosmosDB;

namespace WeatherAPI.Entities;
internal class WeatherCosmosDBInfoEntity : DatabaseInfoEntity
{
    public WeatherCosmosDBInfoEntity(string connectionString) :
        base(connectionString)
    {
    }

    public override string DatabaseName => "weather";
}
