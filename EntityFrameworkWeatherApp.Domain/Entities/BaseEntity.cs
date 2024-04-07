
namespace EntityFrameworkWeatherApp.Domain.Entities;

/// <summary>
/// Base Entity
/// </summary>
public abstract record BaseEntity<TIdType>
{
    public virtual required TIdType Id { get; set; }
}
