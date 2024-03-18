namespace WeatherAPI.Requests.DTO;
public class WeatherAPIToCombinedWeatherDTO
{
    // WeatherAPI           Combined
    // location.lat         = coord.lat
    // location.lon         = coord.lat

    // location.name        = location.name
    // location.country     = location.country
    // location.localtime_epoch     = location.localtime

    // current.feelslike_f  = temp.feels_like_k
    // current.feelslike_f  = temp.feels_like_k
    // current.pressure_mb  = temp.pressure_mb
    // current.humidity     = temp.humidity

    // current.text.description  = cond.desc
    // current.text.icon    = cond.icon
    // current.vis_miles    = cond.visibility(miles)
    // current.wind_kph     = cond.wind_speed(mps)
    // current.wind_degree  = cond.wind_degree
    // current.gust_kph    = cond.wind_gust(mps)
    // current.precip_mm    = cond.precip_mm
    // current.cloud        = cond.clouds
}
