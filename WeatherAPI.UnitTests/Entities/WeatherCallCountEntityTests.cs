using WeatherAPI.Entities.Enums;

namespace WeatherAPI.UnitTests.Entities;
public class WeatherCallCountEntityTests(ITestOutputHelper outputHelper)
{
    private readonly ITestOutputHelper _output = outputHelper;

    [Fact]
    public void Given_WeatherCallCountReset_AllEntriesAreZero()
    {
        // Arrange
        var startWeatherCallCount = new WeatherCallCountEntity();
        var weatherCallCount = new WeatherCallCountEntity();

        // Act
        weatherCallCount.AddWeatherCount(WeatherApiNamesEnums.OpenWeather);

        // Assert
        Assert.Equal(1, weatherCallCount.GetWeatherCount(WeatherApiNamesEnums.OpenWeather));

        weatherCallCount.Reset();

        Assert.Equal(startWeatherCallCount.WeatherCallCount,
            weatherCallCount.WeatherCallCount);
    }

    [Fact]
    public void Given_AddWeatherCount_GetWeatherCountIsOne()
    {
        // Arrange
        var weatherCallCount = new WeatherCallCountEntity();

        // Act
        weatherCallCount.AddWeatherCount(WeatherApiNamesEnums.OpenWeather);

        // Assert
        Assert.Equal(1, weatherCallCount.GetWeatherCount(WeatherApiNamesEnums.OpenWeather));
    }

    [Fact]
    public void Given_GetOrderedWeatherApi_IsOrderedByCount()
    {
        // Arrange
        var weatherCallCount = new WeatherCallCountEntity();
        var listWeatherEnum = new List<WeatherApiNamesEnums>() {
            WeatherApiNamesEnums.WeatherApi,
            WeatherApiNamesEnums.OpenWeather,
        };

        // Act
        weatherCallCount.AddWeatherCount(WeatherApiNamesEnums.OpenWeather);

        // Assert
        Assert.Equal(listWeatherEnum, weatherCallCount.GetOrderedWeatherApi());
    }
}
