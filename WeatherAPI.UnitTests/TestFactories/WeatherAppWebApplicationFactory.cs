using Microsoft.AspNetCore.Mvc.Testing;
using WeatherAPI.UnitTests.MockData.Services;

namespace WeatherAPI.UnitTests.TestFactories;
public class WeatherAppWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public IOptions<OpenWeatherOptions> OpenWeatherOptions { get; private set; }
    public IOptions<WeatherAPIOptions> WeatherAPIOptions { get; private set; }

    public Mock<IOpenWeatherHTTPService> OpenWeatherHTTPServiceMock { get; private set; }
    public Mock<IWeatherAPIHTTPService> WeatherAPIHTTPServiceMock { get; private set; }

    public WeatherAppWebApplicationFactory()
    {
        OpenWeatherOptions =
            new OptionsWrapper<OpenWeatherOptions>(new() { APIKey = "" });
        WeatherAPIOptions =
            new OptionsWrapper<WeatherAPIOptions>(new() { APIKey = "" });

        OpenWeatherHTTPServiceMock =
            WeatherAppWebApplicationFactory<TProgram>.SetupOpenWeatherHTTPServiceMock();
        WeatherAPIHTTPServiceMock =
            WeatherAppWebApplicationFactory<TProgram>.SetupWeatherAPIHTTPServiceMock();
    }

    private static Mock<IOpenWeatherHTTPService> SetupOpenWeatherHTTPServiceMock()
    {
        var mock = new Mock<IOpenWeatherHTTPService>();

        mock.Setup(s => s.GetWeatherByLatLong(It.IsAny<LatLongEntity>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(OpenWeatherHTTPServiceMockData.GetWeatherByLatLong("SuccessLondon"));

        mock.Setup(s => s.GetGeoDirect("London",
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(OpenWeatherHTTPServiceMockData.GetGeoDirect("SuccessLondon"));

        mock.Setup(s => s.GetGeoZip("55407",
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(OpenWeatherHTTPServiceMockData.GetGeoZip("Success55407"));

        return mock;
    }

    private static Mock<IWeatherAPIHTTPService> SetupWeatherAPIHTTPServiceMock()
    {
        var mock = new Mock<IWeatherAPIHTTPService>();

        mock.Setup(s => s.GetWeatherByLatLong(It.IsAny<LatLongEntity>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(WeatherAPIHTTPServiceMockData.GetWeatherByLatLong("SuccessLondon"));

        mock.Setup(s => s.GetWeatherByLocationName(It.IsAny<LocationEntity>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(WeatherAPIHTTPServiceMockData.GetWeatherByLocationName("SuccessLondon"));

        return mock;
    }
}
