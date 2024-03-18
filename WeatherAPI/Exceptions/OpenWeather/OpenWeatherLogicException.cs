namespace WeatherAPI.Exceptions.OpenWeather;
public class OpenWeatherLogicException : Exception
{
    public OpenWeatherLogicException() : base() { }
    public OpenWeatherLogicException(string message) : base(message) { }
    public OpenWeatherLogicException(string message, Exception inner) : base(message, inner) { }
}
