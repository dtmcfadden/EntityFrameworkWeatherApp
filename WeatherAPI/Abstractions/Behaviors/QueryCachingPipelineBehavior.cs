using MediatR;
using WeatherAPI.Abstractions.Caching;

namespace WeatherAPI.Abstractions.Behaviors;
internal sealed class QueryCachingPipelineBehavior<TRequest, TResponse>(ICacheService cacheService)
    : IPipelineBehavior<TRequest, TResponse?> where TRequest : ICachedQuery
{
    private readonly ICacheService _cacheService = cacheService;

    public async Task<TResponse?> Handle(TRequest request,
        RequestHandlerDelegate<TResponse?> next,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetOrCreateAsync(
            request.CacheKey,
            _ => next(),
            request.Expiration,
            request.SlidingExpiration,
            request.Size,
            cancellationToken);
    }
}