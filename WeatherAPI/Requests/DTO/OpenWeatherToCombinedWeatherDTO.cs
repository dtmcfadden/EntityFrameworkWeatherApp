namespace WeatherAPI.Requests.DTO;
public class OpenWeatherToCombinedWeatherDTO
{
    // OpenWeather      Combined
    // coord.lat        = coord.lat
    // coord.lon        = coord.lat

    // name             = location.name
    // sys.country      = location.country
    // dt               = location.localtime

    // main.temp        = temp.temp_k
    // main.feels_like  = temp.feels_like_k
    // main.pressure    = temp.pressure_mb
    // main.humidity    = temp.humidity

    // weather.description  = cond.desc
    // weather.icon     = cond.icon
    // main.visibility  = cond.visibility(miles)
    // wind.speed(mps)  = cond.wind_speed(mps)
    // wind.deg         = cond.wind_degree
    // wind.gust        = cond.wind_gust(mps)
    // rain.OneHourVolume  = cond.precip_mm
    // clouds.all       = cond.clouds
}
