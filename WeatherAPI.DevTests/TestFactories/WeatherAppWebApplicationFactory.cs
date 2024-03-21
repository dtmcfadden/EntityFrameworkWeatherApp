using Microsoft.AspNetCore.Hosting;

namespace WeatherAPI.DevTests.TestFactories;
public class WeatherAppWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    // TODO: Get Sender working properly for testing
    public required Mock<ISender> Sender;

    public WebApplicationFactoryClientOptions OpenWeatherClientOptions = new()
    {
        BaseAddress = new Uri("https://api.openweathermap.org/"),
        AllowAutoRedirect = true,
        HandleCookies = true
    };

    public WebApplicationFactoryClientOptions WeatherAPIClientOptions = new()
    {
        BaseAddress = new Uri("http://api.weatherapi.com/v1/"),
        AllowAutoRedirect = true,
        HandleCookies = true
    };

    private static readonly SocketsHttpHandler defaultSocketHandler = new()
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(5),
    };

    protected override void ConfigureClient(HttpClient client)
    {
        client.DefaultRequestHeaders.Add("accept", "application/json");

        base.ConfigureClient(client);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            var weatherAPITestsConfiguration = ConfigurationTestSettings.GetConfigurationSettings();

            config.AddConfiguration(weatherAPITestsConfiguration);
        });

        builder.ConfigureServices(services =>
        {
            var weatherAPITestsConfiguration = ConfigurationTestSettings.GetConfigurationSettings();

            services.Configure<EnvironmentOptions>(options =>
            {
                if (weatherAPITestsConfiguration != null)
                {
                    var owSection = weatherAPITestsConfiguration.GetSection("openweather-apikey");
                    if (owSection.Value != null)
                        options.OpenWeatherApiKey = owSection.Value;

                    var waSection = weatherAPITestsConfiguration.GetSection("weatherapi-apikey");
                    if (waSection.Value != null)
                        options.WeatherAPIApiKey = waSection.Value;
                }
            });

            services.AddHttpClient<GetOpenWeatherGeoDirectQueryTests>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => defaultSocketHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

            services.AddHttpClient<WeatherAPIHTTPService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("http://api.weatherapi.com/v1/");
            })
                .ConfigurePrimaryHttpMessageHandler(() => defaultSocketHandler)
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

            services.AddHttpClient<GeneralHTTPService>()
                .ConfigurePrimaryHttpMessageHandler(() => defaultSocketHandler)
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
        });

        builder.UseEnvironment("Development");
    }
}
