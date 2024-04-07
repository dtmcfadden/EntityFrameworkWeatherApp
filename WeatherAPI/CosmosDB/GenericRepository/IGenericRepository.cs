using EntityFrameworkWeatherApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace WeatherAPI.CosmosDB.GenericRepository;

public interface IGenericRepository<TEntity, TIdType> where TEntity : BaseEntity<TIdType>
{
    public void Add(TEntity entity);

    public Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken);

    public void Update(TEntity entityToUpdate);

    public void Remove(TIdType id);

    public void Remove(TEntity entityToDelete);

    public TEntity? GetById(TIdType id);

    public Task<TEntity?> GetByIdAsync(TIdType id);

    public IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = "");
}
