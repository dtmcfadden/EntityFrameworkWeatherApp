using WeatherAPI.Entities.Enums;

namespace WeatherAPI.Entities.Interface;
public interface IWeatherCallCountEntity
{
    Dictionary<WeatherApiNamesEnums, int> WeatherCallCount { get; set; }

    void AddWeatherCount(WeatherApiNamesEnums apiName);
    List<WeatherApiNamesEnums> GetOrderedWeatherApi();
    int GetWeatherCount(WeatherApiNamesEnums apiName);
    void Reset();
}