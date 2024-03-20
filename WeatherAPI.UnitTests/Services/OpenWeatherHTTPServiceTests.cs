using WeatherAPI.Models.OpenWeather;
using WeatherAPI.UnitTests.TestFactories;

namespace WeatherAPI.UnitTests.Services;
public class OpenWeatherHTTPServiceTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly OpenWeatherHTTPService _openWeatherHTTPService;
    private readonly Mock<IOpenWeatherHTTPService> _mockOpenWeatherHTTPService;
    //private readonly IOptions<OpenWeatherOptions>? _openWeatherOptions;
    //private readonly HttpClient _client = new();

    public OpenWeatherHTTPServiceTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        _mockOpenWeatherHTTPService = _factory.OpenWeatherHTTPServiceMock;

        _openWeatherHTTPService = new OpenWeatherHTTPService(
                _factory.OpenWeatherOptions,
                _factory.CreateClient());
    }

    [Theory]
    [InlineData(42.3478f, -71.0466f)]
    public async Task Given_OpenWeatherGetWeatherByLatLong_GetsResultIsSuccess(float latitude, float longitude)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Act
        var result = await _mockOpenWeatherHTTPService.Object.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<OpenWeatherDataModel>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(null, -71.0466f, "OpenWeather.LatOrLongIsNull", ",-71.0466")]
    public async Task Given_OpenWeatherGetWeatherByLatLong_GetResultIsFailure(
        float? latitude,
        float? longitude,
        string errorCode,
        string logMessage)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Act
        var result = await _openWeatherHTTPService.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.Equal(result.GetError?.Code, errorCode);
        Assert.Equal(result.GetError?.LogMessage, logMessage);
        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task GivenAPIKeyIsEmpty_OpenWeatherGetWeatherByLatLong_GetResultIsFailure()
    {
        // Arrange
        var latLongEntity = new LatLongEntity(1, 1);
        var owOptions = _factory.OpenWeatherOptions;
        owOptions.Value.APIKey = "";

        var owHTTPService = new OpenWeatherHTTPService(
                owOptions,
                _factory.CreateClient());

        // Act
        var result = await owHTTPService.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.Equal("OpenWeather.APIKeyIsMissing", result.GetError?.Code);
        Assert.Null(result.GetError?.LogMessage);
        Assert.True(result.IsFailure);
    }

    //[Theory]
    //[InlineData("", "OpenWeather.GeoDirectNotValidLocation", "")]
    //public async Task Given_OpenWeatherGetGeoDirect_GetResultIsFailure(string query, string errorCode, string logMessage)
    //{
    //    // Arrange

    //    // Act
    //    var result = await _mockOpenWeatherHTTPService.Object.GetGeoDirect(query);
    //    _output.WriteLine(JsonSerializer.Serialize(result.Value));
    //    _output.WriteLine(JsonSerializer.Serialize(result.GetError));

    //    /// Assert
    //    Assert.Equal(result.GetError?.Code, errorCode);
    //    Assert.Equal(result.GetError?.LogMessage, logMessage);
    //    Assert.True(result.IsFailure);
    //}

    //[Theory]
    //[InlineData("E14,GB")]
    //[InlineData("55407")]
    //public async Task Given_OpenWeatherGetGeoZip_GetsResultIsSuccess(string query)
    //{
    //    // Arrange

    //    // Act
    //    var result = await _mockOpenWeatherHTTPService.Object.GetGeoZip(query);
    //    _output.WriteLine(JsonSerializer.Serialize(result.Value));
    //    _output.WriteLine(JsonSerializer.Serialize(result.GetError));

    //    /// Assert
    //    Assert.IsType<Result<OpenWeatherGeoZipModel>>(result);
    //    Assert.True(result.IsSuccess);
    //}

    //[Theory]
    //[InlineData("", "OpenWeather.ErrorResponseFromOpenWeather", "400:Nothing to geocode")]
    //[InlineData("vvee234vw", "OpenWeather.ErrorResponseFromOpenWeather", "404:not found")]
    //public async Task Given_OpenWeatherGetGeoZip_GetsResultIsFailure(string query, string errorCode, string logMessage)
    //{
    //    // Arrange

    //    // Act
    //    var result = await _mockOpenWeatherHTTPService.Object.GetGeoZip(query);
    //    _output.WriteLine(JsonSerializer.Serialize(result.Value));
    //    _output.WriteLine(JsonSerializer.Serialize(result.GetError));

    //    /// Assert
    //    Assert.Equal(result.GetError?.Code, errorCode);
    //    Assert.Equal(result.GetError?.LogMessage, logMessage);
    //    Assert.True(result.IsFailure);
    //}

    public void Dispose()
    {
        //_client.Dispose();
        //_factory.Dispose();
    }
}
