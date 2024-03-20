

namespace WeatherAPI.DevTests.Services;
public class OpenWeatherHTTPServiceTests : IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    private readonly OpenWeatherHTTPService _openWeatherHTTPService;
    private readonly IOptions<OpenWeatherOptions>? _openWeatherOptions;
    private readonly HttpClient _client = new();

    public OpenWeatherHTTPServiceTests(ITestOutputHelper outputHelper, WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        using (var scope = _factory.Services.CreateScope())
        {
            _client.BaseAddress = _factory.OpenWeatherClientOptions.BaseAddress;

            _openWeatherOptions = scope.ServiceProvider.GetRequiredService<IOptions<OpenWeatherOptions>>();
            _openWeatherHTTPService = new OpenWeatherHTTPService(_openWeatherOptions, _client);
        }
    }

    [Theory]
    [InlineData(42.3478f, -71.0466f)]
    public async Task Given_OpenWeatherGetWeatherByLatLong_GetsResultIsSuccess(float latitude, float longitude)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        // Act
        var result = await _openWeatherHTTPService.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<OpenWeatherDataModel>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(42.3478f, -100000000000000000000000f, "OpenWeather.ErrorResponseFromOpenWeather", "400:wrong longitude")]
    [InlineData(100000000000000000000000f, 42.3478f, "OpenWeather.ErrorResponseFromOpenWeather", "400:wrong latitude")]
    public async Task Given_OpenWeatherGetWeatherByLatLong_GetResultIsFailure(float latitude, float longitude, string errorCode, string logMessage)
    {
        // Arrange
        var latLongEntity = new LatLongEntity(latitude, longitude);

        /// Act
        var result = await _openWeatherHTTPService.GetWeatherByLatLong(latLongEntity);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.Equal(result.GetError?.Code, errorCode);
        Assert.Equal(result.GetError?.LogMessage, logMessage);
        Assert.True(result.IsFailure);
    }

    [Theory]
    [InlineData("London")]
    public async Task Given_OpenWeatherGetGeoDirect_GetsResultIsSuccess(string query)
    {
        // Arrange

        // Act
        var result = await _openWeatherHTTPService.GetGeoDirect(query);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<List<OpenWeatherGeoDirectModel>>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("", "OpenWeather.GeoDirectNotValidLocation", "")]
    [InlineData("Minneapolis, MN", "OpenWeather.GeoDirectNotValidLocation", "Minneapolis, MN")]
    public async Task Given_OpenWeatherGetGeoDirect_GetResultIsFailure(string query, string errorCode, string logMessage)
    {
        // Arrange

        // Act
        var result = await _openWeatherHTTPService.GetGeoDirect(query);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.Equal(result.GetError?.Code, errorCode);
        Assert.Equal(result.GetError?.LogMessage, logMessage);
        Assert.True(result.IsFailure);
    }

    [Theory]
    [InlineData("E14,GB")]
    [InlineData("55407")]
    public async Task Given_OpenWeatherGetGeoZip_GetsResultIsSuccess(string query)
    {
        // Arrange

        // Act
        var result = await _openWeatherHTTPService.GetGeoZip(query);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<OpenWeatherGeoZipModel>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("", "OpenWeather.ErrorResponseFromOpenWeather", "400:Nothing to geocode")]
    [InlineData("vvee234vw", "OpenWeather.ErrorResponseFromOpenWeather", "404:not found")]
    public async Task Given_OpenWeatherGetGeoZip_GetsResultIsFailure(string query, string errorCode, string logMessage)
    {
        // Arrange

        // Act
        var result = await _openWeatherHTTPService.GetGeoZip(query);
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
