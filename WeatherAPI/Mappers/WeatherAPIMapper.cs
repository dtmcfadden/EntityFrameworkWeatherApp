using WeatherAPI.Models.CombinedWeather;
using WeatherAPI.Models.WeatherAPI;

namespace WeatherAPI.Mappers;
public static class WeatherAPIMapper
{
    public static CombinedWeatherDataModel? AsCombinedWeatherData(this WeatherAPICurrentModel waCurrentModel)
    {
        if (waCurrentModel is null)
            return default;

        return new CombinedWeatherDataModel()
        {
            ApiSource = "WeatherAPI",
            CombinedWeatherTemperatureModel = new()
            {
                Kelvin = waCurrentModel.Current.TempC + 273.15f,
                FeelsLikeKelvin = waCurrentModel.Current.FeelsLikeC + 273.15f,
                PressureMillibar = waCurrentModel.Current.PressureMb,
                Humidity = waCurrentModel.Current.Humidity
            },
            CombinedWeatherCoordModel = new()
            {
                Latitude = waCurrentModel.Location.Latitude,
                Longitude = waCurrentModel.Location.Longitude
            },
            CombinedWeatherConditionModel = new()
            {
                Description = waCurrentModel.Current.Condition.Text,
                Icon = waCurrentModel.Current.Condition.Icon,
                Visibility = waCurrentModel.Current.VisibilityMiles,
                WindSpeed = waCurrentModel.Current.WindMph,
                WindDegree = waCurrentModel.Current.WindDegree,
                WindGust = waCurrentModel.Current.GustMph,
                PrecipitationMm = waCurrentModel.Current.PrecipMm,
                Clouds = waCurrentModel.Current.Cloud
            },
            CombinedWeatherLocationModel = new()
            {
                Name = waCurrentModel.Location.Name,
                Country = waCurrentModel.Location.Country,
                LocalTime = waCurrentModel.Location.LocaltimeEpoch,
            }
        };
    }
}
