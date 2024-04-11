using WeatherAPI.Entities.Enums;
using WeatherAPI.Entities.Interface;

namespace WeatherAPI.Entities;
public class WeatherCallCountEntity : IWeatherCallCountEntity
{
    public Dictionary<WeatherApiNamesEnums, int> WeatherCallCount { get; set; } = [];

    public WeatherCallCountEntity()
    {
        foreach (WeatherApiNamesEnums name in Enum.GetValues(typeof(WeatherApiNamesEnums)))
        {
            WeatherCallCount.Add(name, 0);
        }
    }

    public void Reset()
    {
        foreach (var weather in WeatherCallCount)
        {
            WeatherCallCount[weather.Key] = 0;
        }
    }

    public void AddWeatherCount(WeatherApiNamesEnums apiName)
    {
        WeatherCallCount[apiName] += 1;
    }

    public int GetWeatherCount(WeatherApiNamesEnums apiName)
    {
        return WeatherCallCount[apiName];
    }

    public List<WeatherApiNamesEnums> GetOrderedWeatherApi()
    {
        var sortedWeatherapi = from name in WeatherCallCount orderby name.Value select name.Key;
        return sortedWeatherapi.ToList();
    }
}
