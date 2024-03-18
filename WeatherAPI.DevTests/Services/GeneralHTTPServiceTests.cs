namespace WeatherAPI.DevTests.Services;
public class GeneralHTTPServiceTests
{
    private readonly ITestOutputHelper _output;
    private readonly GeneralHTTPService _generalHTTPService;
    private readonly HttpClient _client;


    public GeneralHTTPServiceTests(ITestOutputHelper outputHelper)
    {
        _client = new HttpClient();

        _generalHTTPService = new GeneralHTTPService(_client);
        _output = outputHelper;
    }

    [Theory]
    [InlineData("https://jsonplaceholder.typicode.com/todos")]
    public void Given_TomorrowHTTPService_GetsResult(string url)
    {
        // Arrange
        var result = _generalHTTPService.GetURLAsync(url);
        _output.WriteLine(JsonSerializer.Serialize(result));

        Assert.NotNull(result);
    }
}
