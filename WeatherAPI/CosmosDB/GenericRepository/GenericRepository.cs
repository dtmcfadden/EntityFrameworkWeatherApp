using EntityFrameworkWeatherApp.Domain.Entities;
using EntityFrameworkWeatherApp.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using WeatherAPI.Repositories;

namespace WeatherAPI.CosmosDB.GenericRepository;

// https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-1/

public abstract class GenericRepository<TEntity, TIdType> :
    IGenericRepository<TEntity, TIdType>
    where TEntity : BaseEntity<TIdType>
{
    protected readonly WeatherDBContext DBContext;
    protected readonly DbSet<TEntity> _dbSet;
    private readonly string DBName;
    //private bool IsInitialized { get; set; } = false;

    protected WeatherDBContext Context => DBContext;

    public GenericRepository(DbContextOptions<WeatherDBContext> options, DBStatus dBStatus)
    {
        DBContext = new WeatherDBContext(options);

        DBName = DBContext.DbName;

        InitializeDB(dBStatus);

        _dbSet = DBContext.Set<TEntity>();
    }

    public GenericRepository(WeatherDBContext dbContext, DBStatus dBStatus)
    {
        DBContext = dbContext;
        DBName = DBContext.DbName;

        InitializeDB(dBStatus);

        _dbSet = DBContext.Set<TEntity>();
    }

    private void InitializeDB(DBStatus dBStatus)
    {
        var isDatabaseInitialized = dBStatus.IsDatabaseInitialized;

        if (isDatabaseInitialized.ContainsKey(DBName) == false)
            isDatabaseInitialized.Add(DBName, false);

        if (EnvironmentMethods.IsDevelopment == true && isDatabaseInitialized[DBName] == false)
        {
            if (isDatabaseInitialized[DBName] == false)
                isDatabaseInitialized[DBName] = true;

            DBContext.Database.EnsureCreated();
        }


    }

    public void Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));

        _dbSet.Add(entity);
    }

    public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));

        return await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);

        DBContext.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(TIdType id)
    {
        ArgumentNullException.ThrowIfNull(nameof(id));

        var entityToDelete = GetById(id) ?? throw new Exception("Entity not found");

        _dbSet.Remove(entityToDelete);
    }

    public void Remove(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));

        if (Context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public TEntity? GetById(TIdType id)
    {
        return _dbSet.Find(id);
    }

    public Task<TEntity?> GetByIdAsync(TIdType id)
    {
        return _dbSet.SingleOrDefaultAsync(x => x.Equals(id));
    }

    public IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (!string.IsNullOrEmpty(includeProperties) && !string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            return orderBy(query).AsEnumerable();
        }
        else
        {
            return query.AsEnumerable();
        }
    }
}
