using WeatherAPI.Requests.Queries.OpenWeather;
using WeatherAPI.UnitTests.TestFactories;

namespace WeatherAPI.UnitTests.Requests.Queries.OpenWeather;
public class GetOpenWeatherWeatherByLocationNameQueryTests :
    IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;

    private readonly GetOpenWeatherWeatherByLocationNameHandler _getOpenWeatherWeatherByLocationNameHandler;

    public GetOpenWeatherWeatherByLocationNameQueryTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        _getOpenWeatherWeatherByLocationNameHandler = new GetOpenWeatherWeatherByLocationNameHandler(
            _factory.SenderMock.Object,
            _factory.OpenWeatherHTTPServiceMock.Object,
            _factory.LocationStringMatches
            );
    }

    [Theory]
    [InlineData("", "OpenWeather.LocationIsEmpty", null)]
    public async Task Given_OpenWeatherGetWeatherByLocationName_ThrowsOpenWeatherAPICallException(string locationName, string errorCode, string? logMessage)
    {
        // Arrange
        var weatherByLocationNameQuery = new GetOpenWeatherWeatherByLocationNameQuery(locationName);

        // Act
        var result = await _getOpenWeatherWeatherByLocationNameHandler.Handle(
            weatherByLocationNameQuery, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result));

        /// Assert
        Assert.True(result.IsFailure);
        Assert.Equal(result.GetError?.Code, errorCode);
        Assert.Equal(result.GetError?.LogMessage, logMessage);
    }

    public void Dispose()
    {
        _factory.Dispose();
        //_client.Dispose();
    }
}
