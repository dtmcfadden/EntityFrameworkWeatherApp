using EntityFrameworkWeatherApp.Infrastructure.Common;
using WeatherAPI.CosmosDB.IRepository;
using WeatherAPI.CosmosDB.Repository;
using WeatherAPI.Repositories;

namespace WeatherAPI.CosmosDB.UnitOfWork;
public class UnitOfWork : IUnitOfWork<WeatherDBContext>, IDisposable
{
    private readonly WeatherDBContext _context;
    private readonly DBStatus _dBStatus;
    private bool disposed = false;
    private IWeatherRequestHistoryRepository? _weatherRequestHistoryRepository;

    public IWeatherRequestHistoryRepository WeatherRequestHistoryRepository
    {
        get
        {
            return _weatherRequestHistoryRepository ??=
                new WeatherRequestHistoryRepository(_context, _dBStatus);
        }
    }

    public UnitOfWork(WeatherDBContext context, DBStatus dBStatus)
    {
        _context = context;
        _dBStatus = dBStatus;
    }

    [Time]
    public int Save()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
