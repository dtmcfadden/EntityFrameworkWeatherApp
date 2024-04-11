namespace WeatherAPI.Exceptions.CombinedWeather;
public class CombinedWeatherEmptyException : Exception
{
    public CombinedWeatherEmptyException() : base() { }
    public CombinedWeatherEmptyException(string message) : base(message) { }
    public CombinedWeatherEmptyException(string message, Exception inner) : base(message, inner) { }
}
