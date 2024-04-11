namespace WeatherAPI.DevTests.Requests.Queries.WeatherAPI;
public class GetWeatherAPIWeatherByLocationNameQueryTests :
    IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly WeatherAPIHTTPService _weatherAPIHTTPService;
    private readonly IOptions<EnvironmentOptions>? _environmentOptions;
    private readonly HttpClient _client = new();
    private readonly GetWeatherAPIWeatherByLocationNameHandler _getWeatherAPIWeatherByLocationNameHandler;

    public GetWeatherAPIWeatherByLocationNameQueryTests(ITestOutputHelper outputHelper,
        WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        using (var scope = _factory.Services.CreateScope())
        {
            _client.BaseAddress = _factory.WeatherAPIClientOptions.BaseAddress;

            _environmentOptions = scope.ServiceProvider.GetRequiredService<IOptions<EnvironmentOptions>>();
            _weatherAPIHTTPService = new WeatherAPIHTTPService(
                _environmentOptions, _client, _factory.WeatherCallCountEntityMock.Object);
            _getWeatherAPIWeatherByLocationNameHandler = new GetWeatherAPIWeatherByLocationNameHandler(_weatherAPIHTTPService);
        }
    }

    [Theory]
    [InlineData("London")]
    public async Task Given_GetWeatherAPIWeatherByLocationNameHandler_GetsResultIsSuccess(string LocationName)
    {
        // Arrange
        var request = new GetWeatherAPIWeatherByLocationNameQuery(LocationName);

        // Act
        var result = await _getWeatherAPIWeatherByLocationNameHandler.Handle(request, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<WeatherAPICurrentModel?>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("",
        "LocationEntity.LocationEntityValidationError",
        "",
        new string[] {
            "'Location' must be between 2 and 100 characters. You entered 0 characters.",
            "Please use only letters, numbers, or comma." })]
    [InlineData("%$AVAEE!11",
        "LocationEntity.LocationEntityValidationError",
        "%$AVAEE!11",
        new string[] { "Please use only letters, numbers, or comma." })]
    [InlineData("A",
        "LocationEntity.LocationEntityValidationError",
        "A",
        new string[] { "'Location' must be between 2 and 100 characters. You entered 1 characters." })]
    public async Task Given_GetWeatherAPIWeatherByLocationNameHandler_GetsResultIsFailure(
        string LocationName,
        string errorCode,
        string logMessage,
        string[] validationFailures)
    {
        // Arrange
        var request = new GetWeatherAPIWeatherByLocationNameQuery(LocationName);

        // Act
        var result = await _getWeatherAPIWeatherByLocationNameHandler.Handle(request, CancellationToken.None);
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
