namespace WeatherAPI.Exceptions.OpenWeather;
public class OpenWeatherEntryException : Exception
{
    public OpenWeatherEntryException() : base() { }
    public OpenWeatherEntryException(string message) : base(message) { }
    public OpenWeatherEntryException(string message, Exception inner) : base(message, inner) { }
}
