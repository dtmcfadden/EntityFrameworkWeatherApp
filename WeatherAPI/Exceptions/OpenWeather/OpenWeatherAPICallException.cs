namespace WeatherAPI.Exceptions.OpenWeather;
public class OpenWeatherAPICallException : Exception
{
    public OpenWeatherAPICallException() : base() { }
    public OpenWeatherAPICallException(string message) : base(message) { }
    public OpenWeatherAPICallException(string message, Exception inner) : base(message, inner) { }
}
