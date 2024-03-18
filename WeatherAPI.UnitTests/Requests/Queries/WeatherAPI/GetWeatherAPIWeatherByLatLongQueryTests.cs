using WeatherAPI.Models.WeatherAPI;
using WeatherAPI.Requests.Queries.WeatherAPI;
using WeatherAPI.UnitTests.TestFactories;

namespace WeatherAPI.UnitTests.Requests.Queries.WeatherAPI;
public class GetWeatherAPIWeatherByLatLongQueryTests :
    IClassFixture<WeatherAppWebApplicationFactory<Program>>, IDisposable
{
    private readonly WeatherAppWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;
    //private readonly WeatherAPIHTTPService _weatherAPIHTTPService;
    //private readonly IOptions<WeatherAPIOptions>? _weatherAPIOptions;
    //private readonly HttpClient _client = new();
    private readonly GetWeatherAPIWeatherByLatLongHandler _getWeatherAPIWeatherByLatLongHandler;

    public GetWeatherAPIWeatherByLatLongQueryTests(ITestOutputHelper outputHelper,
        WeatherAppWebApplicationFactory<Program> factory)
    {
        _output = outputHelper;
        _factory = factory;

        _getWeatherAPIWeatherByLatLongHandler = new GetWeatherAPIWeatherByLatLongHandler(_factory.WeatherAPIHTTPServiceMock.Object);
    }

    [Theory]
    [InlineData(42.3478f, -71.0466f)]
    public async Task Given_GetWeatherAPIWeatherByLatLongHandler_GetsResultIsSuccess(float Latitude, float Longitude)
    {
        // Arrange
        var request = new GetWeatherAPIWeatherByLatLongQuery(Latitude, Longitude);

        // Act
        var result = await _getWeatherAPIWeatherByLatLongHandler.Handle(request, CancellationToken.None);
        _output.WriteLine(JsonSerializer.Serialize(result.Value));
        _output.WriteLine(JsonSerializer.Serialize(result.GetError));

        /// Assert
        Assert.IsType<Result<WeatherAPICurrentModel?>>(result);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData(null,
        null,
        "LatLongEntity.LatLongEntityValidationError",
        ",",
        new string[] { "'Latitude' must not be empty.", "'Longitude' must not be empty." })]
    [InlineData(null,
        -100000000000000000000000f,
        "LatLongEntity.LatLongEntityValidationError",
        ",-1E+23",
        new string[] { "'Latitude' must not be empty.", "'Longitude' must be between -180 and 180. You entered -1E+23." })]
    [InlineData(-100000000000000000000000f,
        null,
        "LatLongEntity.LatLongEntityValidationError",
        "-1E+23,",
        new string[] { "'Longitude' must not be empty.", "'Latitude' must be between -90 and 90. You entered -1E+23." })]
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
    public async Task Given_GetWeatherAPIWeatherByLatLongHandler_GetsResultIsFailure(
        float? latitude,
        float? longitude,
        string errorCode,
        string logMessage,
        string[] validationFailures)
    {
        // Arrange
        var request = new GetWeatherAPIWeatherByLatLongQuery(latitude, longitude);

        // Act
        var result = await _getWeatherAPIWeatherByLatLongHandler.Handle(request, CancellationToken.None);
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
        //_client.Dispose();
    }
}
