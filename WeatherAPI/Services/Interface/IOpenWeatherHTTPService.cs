using WeatherAPI.Models.OpenWeather;

namespace WeatherAPI.Services.Interface;
public interface IOpenWeatherHTTPService : IBaseWeatherHTTPService<OpenWeatherDataModel>
{
    Task<Result<List<OpenWeatherGeoDirectModel>>> GetGeoDirect(string LocationQuery, CancellationToken cancellationToken = default);
    Task<Result<OpenWeatherGeoZipModel>> GetGeoZip(string ZipQuery, CancellationToken cancellationToken = default);
}