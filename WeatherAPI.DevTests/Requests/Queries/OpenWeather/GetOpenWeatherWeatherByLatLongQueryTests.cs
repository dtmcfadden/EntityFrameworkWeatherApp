namespace WeatherAPI.DevTests.Requests.Queries.OpenWeather;
public class GetOpenWeatherWeatherByLatLongQueryTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly OpenWeatherHTTPService _openWeatherHTTPService;
    private readonly IOptions<EnvironmentOptions>? _environmentOptions;
    private readonly HttpClient _client = new();
    private readonly GetOpenWeatherWeatherByLatLongHandler _getOpenWeatherWeatherByLatLongHandler;

    public GetOpenWeatherWeatherByLatLongQueryTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        using (var scope = _factory.Services.CreateScope())
        {
            _client.BaseAddress = _factory.OpenWeatherClientOptions.BaseAddress;

            _environmentOptions = scope.ServiceProvider.GetRequiredService<IOptions<EnvironmentOptions>>();
            _openWeatherHTTPService = new OpenWeatherHTTPService(
                _environmentOptions, _client, _factory.WeatherCallCountEntityMock.Object);
            _getOpenWeatherWeatherByLatLongHandler = new GetOpenWeatherWeatherByLatLongHandler(_openWeatherHTTPService);
        }
    }

    [Theory]
    [InlineData(42.3478f, -71.0466f)]
    public async Task Given_GetWeatherByLatLongHandler_GetsResultIsSuccess(float Latitude, float Longitude)
    {
        // Arrange
        var locationQuery = new GetOpenWeatherWeatherByLatLongQuery(Latitude, Longitude);

        // Act
        var result = await _getOpenWeatherWeatherByLatLongHandler.Handle(locationQuery, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<OpenWeatherDataModel?>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(null,
        -100000000000000000000000f,
        "LatLongEntity.LatLongEntityValidationError",
        ",-1E+23",
        new string[] { "'Latitude' must not be empty.", "'Longitude' must be between -180 and 180. You entered -1E+23." })]
    [InlineData(
        42.3478f,
        -100000000000000000000000f,
        "LatLongEntity.LatLongEntityValidationError",
        "42.3478,-1E+23",
        new string[] { "'Longitude' must be between -180 and 180. You entered -1E+23." })]
    [InlineData(
        100000000000000000000000f,
        42.3478f,
        "LatLongEntity.LatLongEntityValidationError",
        "1E+23,42.3478",
        new string[] { "'Latitude' must be between -90 and 90. You entered 1E+23." })]
    public async Task Given_GetWeatherByLatLongHandler_GetsResultIsFailure(
        float? latitude,
        float? longitude,
        string errorCode,
        string logMessage,
        string[] validationFailures)
    {
        // Arrange
        var locationQuery = new GetOpenWeatherWeatherByLatLongQuery(latitude, longitude);

        // Act
        var result = await _getOpenWeatherWeatherByLatLongHandler.Handle(locationQuery, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.True(result.IsFailure);
        Assert.Equal(result.GetError?.Code, errorCode);
        Assert.Equal(result.GetError?.LogMessage, logMessage);

        Assert.Equal(result?.GetValidationResult?.Errors.Count, validationFailures.Length);
        result?.GetValidationResult?.Errors.ForEach(e => Assert.Contains(e.ErrorMessage, validationFailures));
    }

    public void Dispose()
    {
        //_factory.Dispose();
        _client.Dispose();
    }
}
