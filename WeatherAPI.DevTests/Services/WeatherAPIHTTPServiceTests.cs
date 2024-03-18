namespace WeatherAPI.DevTests.Services;
public class WeatherAPIHTTPServiceTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly WeatherAPIHTTPService _weatherAPIHTTPService;
    private readonly IOptions<WeatherAPIOptions>? _weatherAPIOptions;
    private readonly HttpClient _client = new();

    public WeatherAPIHTTPServiceTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        using (var scope = _factory.Services.CreateScope())
        {
            _client.BaseAddress = _factory.WeatherAPIClientOptions.BaseAddress;

            _weatherAPIOptions = scope.ServiceProvider.GetRequiredService<IOptions<WeatherAPIOptions>>();
            _weatherAPIHTTPService = new WeatherAPIHTTPService(_weatherAPIOptions, _client);
        }
    }

    [Theory]
    [InlineData(42.3478f, -71.0466f)]
    public async Task Given_GetWeatherByLatLong_GetsResult(float latitude, float longitude)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Arrange
        var result = await _weatherAPIHTTPService.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<WeatherAPICurrentModel>>(result);
        Assert.Equal((int)result?.Value?.Location?.Latitude, (int)latitude);
        Assert.Equal((int)result?.Value?.Location?.Longitude, (int)longitude);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(42.3478f, -100000000000000000000000f, "WeatherAPI.LatOrLongIsInvalid")]
    [InlineData(100000000000000000000000f, 42.3478f, "WeatherAPI.LatOrLongIsInvalid")]
    public async Task Given_GetWeatherByLatLong_GetResultIsFailure(float latitude, float longitude, string errorCode)
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
        //Assert.NotEqual((int)result?.Value?.Location?.Latitude, (int)latitude);
        //Assert.NotEqual((int)result?.Value?.Location?.Longitude, (int)longitude);
        //Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("", "WeatherAPI.ErrorResponseFromWeatherAPI", "1003:Parameter q is missing.")]
    [InlineData("bsblhsbhsb", "WeatherAPI.ErrorResponseFromWeatherAPI", "1006:No matching location found.")]
    public async Task Given_GetWeatherByLocationName_GetResultIsFailure(string locationName, string errorCode, string logMessage)
    {
        // Arrange
        var locationEntity = new LocationEntity(locationName);

        // Act
        var result = await _weatherAPIHTTPService.GetWeatherByLocationName(locationEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.Equal(result.GetError?.Code, errorCode);
        Assert.Equal(result.GetError?.LogMessage, logMessage);
        Assert.True(result.IsFailure);
    }

    public void Dispose()
    {
        _client.Dispose();
        //_factory.Dispose();
    }
}
