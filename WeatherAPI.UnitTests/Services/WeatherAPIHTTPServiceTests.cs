using WeatherAPI.Models.WeatherAPI;
using WeatherAPI.UnitTests.TestFactories;

namespace WeatherAPI.UnitTests.Services;
public class WeatherAPIHTTPServiceTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly WeatherAPIHTTPService _weatherAPIHTTPService;
    private readonly Mock<IWeatherAPIHTTPService> _mockWeatherAPIHTTPService;
    //private readonly IOptions<WeatherAPIOptions>? _weatherAPIOptions;
    //private readonly HttpClient _client = new();

    public WeatherAPIHTTPServiceTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        _mockWeatherAPIHTTPService = _factory.WeatherAPIHTTPServiceMock;

        _weatherAPIHTTPService = new WeatherAPIHTTPService(
                _factory.EnvironmentOptions,
                _factory.CreateClient());
    }

    [Theory]
    [InlineData(51.52f, -0.11f)]
    public async Task Given_GetWeatherByLatLong_GetsResult(float latitude, float longitude)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Arrange
        var result = await _mockWeatherAPIHTTPService.Object.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<WeatherAPICurrentModel>>(result);
        Assert.NotNull(result.Value);
        Assert.NotNull(result.Value.Location);
        if (result.Value.Location?.Latitude != null)
        {
            Assert.NotNull(result.Value.Location?.Latitude);
            Assert.Equal((int)result.Value.Location.Latitude, (int)latitude);
        }
        if (result.Value.Location?.Longitude != null)
        {
            Assert.NotNull(result.Value.Location?.Longitude);
            Assert.Equal((int)result.Value.Location.Longitude, (int)longitude);
        }

        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(42.3478f, null, "WeatherAPI.LatOrLongIsNull")]
    [InlineData(null, 42.3478f, "WeatherAPI.LatOrLongIsNull")]
    [InlineData(null, null, "WeatherAPI.LatOrLongIsNull")]
    public async Task Given_GetWeatherByLatLong_GetResultIsFailure(
        float? latitude,
        float? longitude,
        string errorCode)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        /// Act
        var result = await _weatherAPIHTTPService.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<WeatherAPICurrentModel>>(result);
        Assert.Equal(result.GetError?.Code, errorCode);
        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task GivenAPIKeyIsEmpty_WeatherAPIGetWeatherByLatLong_GetResultIsFailure()
    {
        // Arrange
        var latLongEntity = new LatLongEntity(1, 1);
        var waOptions = _factory.EnvironmentOptions;
        waOptions.Value.WeatherAPIApiKey = "";

        var waHTTPService = new WeatherAPIHTTPService(
                waOptions,
                _factory.CreateClient());

        // Act
        var result = await waHTTPService.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.Equal("WeatherAPI.APIKeyIsMissing", result.GetError?.Code);
        Assert.Null(result.GetError?.LogMessage);
        Assert.True(result.IsFailure);
    }

    //[Theory]
    //[InlineData("", "WeatherAPI.ErrorResponseFromWeatherAPI", "1003:Parameter q is missing.")]
    //[InlineData("bsblhsbhsb", "WeatherAPI.ErrorResponseFromWeatherAPI", "1006:No matching location found.")]
    //public async Task Given_GetWeatherByLocationName_GetResultIsFailure(string locationName, string errorCode, string logMessage)
    //{
    //    // Arrange
    //    var locationEntity = new LocationEntity(locationName);

    //    // Act
    //    var result = await _weatherAPIHTTPService.GetWeatherByLocationName(locationEntity);
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
