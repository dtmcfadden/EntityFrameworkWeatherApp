namespace WeatherAPI.DevTests.Requests.Queries.OpenWeather;
public class GetOpenWeatherGeoZipQueryTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly OpenWeatherHTTPService _openWeatherHTTPService;
    private readonly IOptions<EnvironmentOptions>? _environmentOptions;
    private readonly HttpClient _client = new();
    private readonly GetOpenWeatherGeoZipHandler _getOpenWeatherGeoZipHandler;

    public GetOpenWeatherGeoZipQueryTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        using (var scope = _factory.Services.CreateScope())
        {
            _client.BaseAddress = _factory.OpenWeatherClientOptions.BaseAddress;

            _environmentOptions = scope.ServiceProvider.GetRequiredService<IOptions<EnvironmentOptions>>();
            _openWeatherHTTPService = new OpenWeatherHTTPService(_environmentOptions, _client);
            _getOpenWeatherGeoZipHandler = new GetOpenWeatherGeoZipHandler(_openWeatherHTTPService);
        }
    }

    [Theory]
    [InlineData("E14,GB")]
    [InlineData("55407")]
    public async Task Given_GetOpenWeatherGeoZipHandler_GetsResultIsSuccess(string ZipQuery)
    {
        // Arrange
        var zipQuery = new GetOpenWeatherGeoZipQuery(ZipQuery);

        // Act
        var result = await _getOpenWeatherGeoZipHandler.Handle(zipQuery, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<OpenWeatherGeoZipModel?>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("", "OpenWeather.ZipIsEmpty", "")]
    [InlineData("vvee234vw", "OpenWeather.ErrorResponseFromOpenWeather", "404:not found")]
    public async Task Given_GetOpenWeatherGeoDirectHandler_GetsResultIsFailure(string ZipQuery, string errorCode, string logMessage)
    {
        // Arrange
        var zipQuery = new GetOpenWeatherGeoZipQuery(ZipQuery);

        // Act
        var result = await _getOpenWeatherGeoZipHandler.Handle(zipQuery, CancellationToken.None);
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
        _client.Dispose();
    }
}
