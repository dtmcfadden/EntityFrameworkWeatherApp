namespace WeatherAPI.Abstractions.Caching;

public interface ICacheService
{
    Task<T?> GetOrCreateAsync<T>(
        string CacheKey,
        Func<CancellationToken, Task<T?>> factory,
        TimeSpan? expiration = null,
        TimeSpan? slidingExpiration = null,
        long? size = null,
        CancellationToken cancellationToken = default);
}