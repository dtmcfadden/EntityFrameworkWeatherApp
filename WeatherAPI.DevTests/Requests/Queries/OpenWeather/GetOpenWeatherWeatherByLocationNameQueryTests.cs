using WeatherAPI.Common;

namespace WeatherAPI.DevTests.Requests.Queries.OpenWeather;
public class GetOpenWeatherWeatherByLocationNameQueryTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly OpenWeatherHTTPService _openWeatherHTTPService;
    private readonly Mock<ISender> _sender;
    private readonly IOptions<EnvironmentOptions>? _environmentOptions;
    private readonly LocationStringMatches _locationStringMatches = new();
    private readonly GetOpenWeatherGeoDirectHandler _getOpenWeatherGeoDirectHandler;
    private readonly GetOpenWeatherGeoZipHandler _getOpenWeatherGeoZipHandler;
    private readonly HttpClient _client = new();
    private readonly GetOpenWeatherWeatherByLocationNameHandler _getOpenWeatherWeatherByLocationNameHandler;

    public GetOpenWeatherWeatherByLocationNameQueryTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;
        _sender = _factory.Sender;

        using (var scope = _factory.Services.CreateScope())
        {
            _client.BaseAddress = _factory.OpenWeatherClientOptions.BaseAddress;

            _environmentOptions = scope.ServiceProvider.GetRequiredService<IOptions<EnvironmentOptions>>();
            _openWeatherHTTPService = new OpenWeatherHTTPService(_environmentOptions, _client);
            _getOpenWeatherGeoDirectHandler = new GetOpenWeatherGeoDirectHandler(_openWeatherHTTPService);
            _getOpenWeatherGeoZipHandler = new GetOpenWeatherGeoZipHandler(_openWeatherHTTPService);
            _getOpenWeatherWeatherByLocationNameHandler = new GetOpenWeatherWeatherByLocationNameHandler(
            _sender.Object,
            _openWeatherHTTPService,
            _locationStringMatches);
        }
    }

    //[Theory]
    //[InlineData("London")]
    //[InlineData("Minneapolis, MN, US")]
    //[InlineData("MN, US")]
    //[InlineData("55407")]
    //public async Task Given_OpenWeatherGetWeatherByLocationName_GetsResultIsSuccess(string locationName)
    //{
    //    // Arrange
    //    var weatherByLocationNameQuery = new GetOpenWeatherWeatherByLocationNameQuery(locationName);

    //    // Act
    //    var result = await _getOpenWeatherWeatherByLocationNameHandler.Handle(weatherByLocationNameQuery, CancellationToken.None);
    //    _output.WriteLine(JsonSerializer.Serialize(result));

    //    /// Assert
    //    Assert.IsType<Result<OpenWeatherDataModel?>>(result);
    //    Assert.True(result.IsSuccess);
    //}

    //[Theory]
    //[InlineData("", "OpenWeather.LocationIsEmpty", null)]
    //[InlineData("Minneapolis, MN", "OpenWeather.GeoDirectNotValidLocation", "Minneapolis, MN")]
    //public async Task Given_OpenWeatherGetWeatherByLocationName_ThrowsOpenWeatherAPICallException(string locationName, string errorCode, string? logMessage)
    //{
    //    // Arrange
    //    var weatherByLocationNameQuery = new GetOpenWeatherWeatherByLocationNameQuery(locationName);

    //    // Act
    //    var result = await _getOpenWeatherWeatherByLocationNameHandler.Handle(weatherByLocationNameQuery, CancellationToken.None);
    //    _output.WriteLine(JsonSerializer.Serialize(result));

    //    /// Assert
    //    Assert.True(result.IsFailure);
    //    Assert.Equal(result.GetError?.Code, errorCode);
    //    Assert.Equal(result.GetError?.LogMessage, logMessage);
    //}

    public void Dispose()
    {
        //_factory.Dispose();
        _client.Dispose();
    }
}
