﻿using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using WeatherAPI.Common;
using WeatherAPI.Entities.Interface;
using WeatherAPI.UnitTests.MockData.Services;

namespace WeatherAPI.UnitTests.TestFactories;
public class WeatherAppWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public IOptions<EnvironmentOptions> EnvironmentOptions { get; private set; }

    public Mock<ISender> SenderMock { get; private set; }

    public Mock<IWeatherCallCountEntity> WeatherCallCountEntityMock { get; private set; }

    public Mock<IOpenWeatherHTTPService> OpenWeatherHTTPServiceMock { get; private set; }
    public Mock<IWeatherAPIHTTPService> WeatherAPIHTTPServiceMock { get; private set; }

    public LocationStringMatches LocationStringMatches = new();

    public WeatherAppWebApplicationFactory()
    {
        EnvironmentOptions =
            new OptionsWrapper<EnvironmentOptions>(new()
            {
                OpenWeatherApiKey = "Key",
                WeatherAPIApiKey = "Key",
                WeatherConnectionString = "CS",
                WeatherDatabaseName = "DBName",
            });

        SenderMock = new Mock<ISender>();
        WeatherCallCountEntityMock = new Mock<IWeatherCallCountEntity>();

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

        mock.Setup(s => s.GetGeoDirect("London", It.IsAny<CancellationToken>()))
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
