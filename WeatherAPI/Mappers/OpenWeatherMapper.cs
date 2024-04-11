using WeatherAPI.Models.CombinedWeather;
using WeatherAPI.Models.OpenWeather;

namespace WeatherAPI.Mappers;

public static class OpenWeatherMapper
{
    public static CombinedWeatherDataModel? AsCombinedWeatherData(this OpenWeatherDataModel owDataModel)
    {
        if (owDataModel is null)
            return default;

        return new CombinedWeatherDataModel()
        {
            ApiSource = "OpenWeather",
            CombinedWeatherTemperatureModel = new()
            {
                Kelvin = owDataModel.Main.Temperature,
                FeelsLikeKelvin = owDataModel.Main.FeelsLike,
                PressureMillibar = owDataModel.Main.Pressure,
                Humidity = owDataModel.Main.Humidity
            },
            CombinedWeatherCoordModel = new()
            {
                Latitude = owDataModel.Coordinates.Latitude,
                Longitude = owDataModel.Coordinates.Longitude
            },
            CombinedWeatherConditionModel = new()
            {
                Description = owDataModel.Weather.First().Description,
                Icon = owDataModel.Weather.First().Icon,
                Visibility = owDataModel.Visibility,
                WindSpeed = owDataModel.Wind?.Speed,
                WindDegree = owDataModel.Wind?.Degrees,
                WindGust = owDataModel.Wind?.Gust,
                PrecipitationMm = owDataModel.Rain?.OneHourVolume,
                Clouds = owDataModel.Clouds?.All
            },
            CombinedWeatherLocationModel = new()
            {
                Name = owDataModel.CityName,
                Country = owDataModel.Sys.Country,
                LocalTime = owDataModel.Dt
            }
        };
    }
}
