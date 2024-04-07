using WeatherAPI.CosmosDB.Entities;
using WeatherAPI.CosmosDB.GenericRepository;

namespace WeatherAPI.CosmosDB.IRepository;
public interface IWeatherRequestHistoryRepository :
    IGenericRepository<WeatherRequestHistoryEntity, Guid>
{
}
