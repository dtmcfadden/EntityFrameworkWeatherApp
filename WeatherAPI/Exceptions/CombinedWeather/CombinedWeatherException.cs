namespace WeatherAPI.Exceptions.CombinedWeather;
public class CombinedWeatherException : Exception
{
    public CombinedWeatherException() : base() { }
    public CombinedWeatherException(string message) : base(message) { }
    public CombinedWeatherException(string message, Exception inner) : base(message, inner) { }
}
