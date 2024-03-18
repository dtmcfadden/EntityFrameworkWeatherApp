namespace WeatherAPI.Exceptions.WeatherAPI;
public class WeatherAPIException : Exception
{
    public WeatherAPIException() : base() { }
    public WeatherAPIException(string message) : base(message) { }
    //public WeatherAPIException(string message, OpenWeatherErrorModel? openWeatherError) : base(message) { }
    public WeatherAPIException(string message, Exception inner) : base(message, inner) { }
    //public WeatherAPIException(string message, Exception inner, OpenWeatherErrorModel? openWeatherError) : base(message, inner) { }
}
