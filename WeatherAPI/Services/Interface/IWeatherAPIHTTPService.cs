using WeatherAPI.Models.WeatherAPI;

namespace WeatherAPI.Services.Interface;
public interface IWeatherAPIHTTPService : IBaseWeatherHTTPService<WeatherAPICurrentModel>
{
    Task<Result<WeatherAPICurrentModel>> GetWeatherByLocationName(LocationEntity locationName, CancellationToken cancellationToken = default);
}