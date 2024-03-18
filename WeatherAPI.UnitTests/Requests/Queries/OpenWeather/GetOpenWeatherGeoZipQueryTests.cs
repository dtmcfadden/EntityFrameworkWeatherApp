using WeatherAPI.Models.OpenWeather;
using WeatherAPI.Requests.Queries.OpenWeather;
using WeatherAPI.UnitTests.TestFactories;

namespace WeatherAPI.UnitTests.Requests.Queries.OpenWeather;
public class GetOpenWeatherGeoZipQueryTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly GetOpenWeatherGeoZipHandler _getOpenWeatherGeoZipHandler;

    public GetOpenWeatherGeoZipQueryTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        _getOpenWeatherGeoZipHandler = new GetOpenWeatherGeoZipHandler(_factory.OpenWeatherHTTPServiceMock.Object);
    }

    [Fact]
    public async Task Given_GetOpenWeatherGeoZipHandler_GetsResultIsSuccess()
    {
        // Arrange
        var zipQuery = new GetOpenWeatherGeoZipQuery("55407");

        // Act
        var result = await _getOpenWeatherGeoZipHandler.Handle(zipQuery, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<OpenWeatherGeoZipModel?>>(result);
        Assert.True(result.IsSuccess);
    }

    //[Theory]
    //[InlineData("E14,GB")]
    //[InlineData("55407")]
    //public async Task Given_GetOpenWeatherGeoZipHandler_GetsResultIsSuccess(string ZipQuery)
    //{
    //    // Arrange
    //    var zipQuery = new GetOpenWeatherGeoZipQuery(ZipQuery);

    //    // Act
    //    var result = await _getOpenWeatherGeoZipHandler.Handle(zipQuery, CancellationToken.None);
    //    _output.WriteLine(JsonSerializer.Serialize(result.Value));
    //    _output.WriteLine(JsonSerializer.Serialize(result.GetError));

    //    /// Assert
    //    Assert.IsType<Result<OpenWeatherGeoZipModel?>>(result);
    //    Assert.True(result.IsSuccess);
    //}

    //[Theory]
    //[InlineData("E14,GB")]
    //[InlineData("55407")]
    //public async Task Given_GetOpenWeatherGeoZipHandler_GetsResultIsSuccess(string ZipQuery)
    //{
    //    // Arrange
    //    var zipQuery = new GetOpenWeatherGeoZipQuery(ZipQuery);

    //    // Act
    //    var result = await _getOpenWeatherGeoZipHandler.Handle(zipQuery, CancellationToken.None);
    //    _output.WriteLine(JsonSerializer.Serialize(result.Value));
    //    _output.WriteLine(JsonSerializer.Serialize(result.GetError));

    //    /// Assert
    //    Assert.IsType<Result<OpenWeatherGeoZipModel?>>(result);
    //    Assert.True(result.IsSuccess);
    //}

    //[Theory]
    //[InlineData("", "OpenWeather.ZipIsEmpty", "")]
    //[InlineData("vvee234vw", "OpenWeather.ErrorResponseFromOpenWeather", "404:not found")]
    //public async Task Given_GetOpenWeatherGeoDirectHandler_GetsResultIsFailure(string ZipQuery, string errorCode, string logMessage)
    //{
    //    // Arrange
    //    var zipQuery = new GetOpenWeatherGeoZipQuery(ZipQuery);

    //    // Act
    //    var result = await _getOpenWeatherGeoZipHandler.Handle(zipQuery, CancellationToken.None);
    //    _output.WriteLine(JsonSerializer.Serialize(result.Value));
    //    _output.WriteLine(JsonSerializer.Serialize(result.GetError));

    //    /// Assert
    //    Assert.Equal(result.GetError?.Code, errorCode);
    //    Assert.Equal(result.GetError?.LogMessage, logMessage);
    //    Assert.True(result.IsFailure);
    //}

    public void Dispose()
    {
        //_factory.Dispose();
        //_client.Dispose();
    }
}
