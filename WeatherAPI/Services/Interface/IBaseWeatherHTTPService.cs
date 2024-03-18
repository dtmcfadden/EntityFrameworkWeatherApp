namespace WeatherAPI.Services.Interface;
public interface IBaseWeatherHTTPService<T>
{
    Task<Result<T?>> GetWeatherByLatLong(LatLongEntity latLong,
        CancellationToken cancellationToken = default);
}