
using WeatherAPI.Models.OpenWeather;
using WeatherAPI.Requests.Queries.OpenWeather;
using WeatherAPI.UnitTests.TestFactories;

namespace WeatherAPI.UnitTests.Requests.Queries.OpenWeather;
public class GetOpenWeatherGeoDirectQueryTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly GetOpenWeatherGeoDirectHandler _getOpenWeatherGeoDirectHandler;

    public GetOpenWeatherGeoDirectQueryTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        _getOpenWeatherGeoDirectHandler = new GetOpenWeatherGeoDirectHandler(_factory.OpenWeatherHTTPServiceMock.Object);
    }

    [Fact]
    public async Task Given_GetOpenWeatherGeoDirectHandler_GetsResultIsSuccess()
    {
        // Arrange
        var locationQuery = new GetOpenWeatherGeoDirectQuery("London");

        // Act
        var result = await _getOpenWeatherGeoDirectHandler.Handle(locationQuery, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<List<OpenWeatherGeoDirectModel>?>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("", "OpenWeather.LocationIsEmpty", "")]
    //[InlineData("Minneapolis, MN", "OpenWeather.GeoDirectNotValidLocation", "Minneapolis, MN")]
    public async Task Given_GetOpenWeatherGeoDirectHandler_GetsResultIsFailure(string LocationQuery, string errorCode, string logMessage)
    {
        // Arrange
        var locationQuery = new GetOpenWeatherGeoDirectQuery(LocationQuery);

        // Act
        var result = await _getOpenWeatherGeoDirectHandler.Handle(locationQuery, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.Equal(result.GetError?.Code, errorCode);
        Assert.Equal(result.GetError?.LogMessage, logMessage);
        Assert.True(result.IsFailure);
    }

    public void Dispose()
    {
        //_factory.Dispose();
        //_client.Dispose();
    }
}
