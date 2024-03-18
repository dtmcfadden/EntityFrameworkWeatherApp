using MediatR;

namespace WeatherAPI.Abstractions;

public interface ICachedQuery<TResonse> : IRequest<TResonse>, ICachedQuery;
public interface ICachedQuery
{
    string CacheKey { get; }

    TimeSpan? Expiration { get; }
    TimeSpan? SlidingExpiration { get; }
    long? Size { get; }

}

