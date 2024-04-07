using Microsoft.EntityFrameworkCore;
using WeatherAPI.CosmosDB.IRepository;

namespace WeatherAPI.CosmosDB.UnitOfWork;
public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
{
    IWeatherRequestHistoryRepository WeatherRequestHistoryRepository { get; }

    int Save();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
