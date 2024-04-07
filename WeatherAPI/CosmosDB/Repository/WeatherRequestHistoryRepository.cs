using EntityFrameworkWeatherApp.Infrastructure.Common;
using WeatherAPI.CosmosDB.Entities;
using WeatherAPI.CosmosDB.GenericRepository;
using WeatherAPI.CosmosDB.IRepository;
using WeatherAPI.Repositories;

namespace WeatherAPI.CosmosDB.Repository;
public class WeatherRequestHistoryRepository :
    GenericRepository<WeatherRequestHistoryEntity, Guid>,
    IWeatherRequestHistoryRepository
{
    public WeatherRequestHistoryRepository(WeatherDBContext context, DBStatus dBStatus) : base(context, dBStatus)
    {

    }
}
