using Microsoft.Extensions.Caching.Memory;
using WeatherAPI.Abstractions.Caching;

namespace WeatherAPI.Services.Caching;
internal sealed class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);
    private readonly TimeSpan DefaultSlidingExpiration = TimeSpan.FromMinutes(5);

    private readonly long DefaultSize = 1;

    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<T?> GetOrCreateAsync<T>(
        string CacheKey,
        Func<CancellationToken, Task<T?>> factory,
        TimeSpan? expiration = null,
        TimeSpan? slidingExpiration = null,
        long? size = null,
        CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync(CacheKey, entry =>
        {
            entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);
            entry.SetSlidingExpiration(slidingExpiration ?? DefaultSlidingExpiration);
            entry.SetSize(size ?? DefaultSize);

            return factory(cancellationToken);
        });
    }
}
