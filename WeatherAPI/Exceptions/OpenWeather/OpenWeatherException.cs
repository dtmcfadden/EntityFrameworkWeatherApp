namespace WeatherAPI.Exceptions.OpenWeather;

[Serializable]
public class OpenWeatherException : Exception
{
    public OpenWeatherException() : base() { }
    public OpenWeatherException(string message) : base(message) { }
    public OpenWeatherException(string message, Exception inner) : base(message, inner) { }
}
