using Microsoft.AspNetCore.Hosting;

namespace WeatherAPI.DevTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    //public Mock<TomorrowOptions> TomorrowOptionsMock { get; }
    public Mock<ISender> Sender = new();
    //private readonly IConfiguration _configuration;
    //public IOptions<OpenWeatherOptions>? OpenWeatherOptions { get; }
    //public IOptions<WeatherAPIOptions>? WeatherAPIOptions { get; }

    public CustomWebApplicationFactory()
    {
        //TomorrowOptionsMock = new Mock<TomorrowOptions>();
        //Sender = new Mock<ISender>();

        //var currentAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("WeatherAPI.UnitTests"));

        //var assemblyLocation = new Uri(Assembly.GetExecutingAssembly().Location);
        //var assemblyDirectory = new FileInfo(assemblyLocation.AbsolutePath).Directory.FullName;

        //foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
        //{
        //    Assembly assembly = Assembly.Load(assemblyName);
        //    foreach (var type in assembly.GetTypes())
        //    {
        //        Console.WriteLine(type.Name);
        //    }
        //}

        //_configuration = new ConfigurationBuilder()
        //     //.SetBasePath(Directory.GetCurrentDirectory())
        //     //.SetBasePath(assemblyDirectory)
        //     //.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        //     //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //     .AddUserSecrets(Assembly.GetExecutingAssembly())
        //     .AddEnvironmentVariables()
        //     .Build();

        //var weatherAPITestsConfiguration = ConfigurationTestSettings.GetConfigurationSettings();

        //OpenWeatherOptions = weatherAPIConfiguration.GetSection(OpenWeatherOptions.Name);

        //var openWeatherOptions = weatherAPITestsConfiguration.GetSection("OpenWeather").Get<OpenWeatherOptions>();
        //var t = OptionsFactory.Create(openWeatherOptions);

        //OpenWeatherOptions = (IOptions<OpenWeatherOptions>?)openWeatherOptions;

        //var weatherAPIOptions = weatherAPITestsConfiguration.GetSection("WeatherAPI").Get<WeatherAPIOptions>();

        //OpenWeatherOptions = weatherAPITestsConfiguration.GetSection("OpenWeather").Get<IOptions<OpenWeatherOptions>>();
        //WeatherAPIOptions = weatherAPITestsConfiguration.GetSection("WeatherAPI").Get<IOptions<WeatherAPIOptions>>();

        //OpenWeatherOptions = (IOptions<OpenWeatherOptions>?)_configuration.GetSection("OpenWeather").Get<OpenWeatherOptions>();
        //WeatherAPIOptions = (IOptions<WeatherAPIOptions>?)_configuration.GetSection("WeatherAPI").Get<WeatherAPIOptions>();
    }

    //protected override void ConfigureClient(HttpClient client)
    //{
    //    base.ConfigureClient(client);
    //}

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //builder.ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
        //{
        //    var type = typeof(TStartup);
        //    var path = @"C:\\OriginalApplication";

        //    configurationBuilder.AddJsonFile($"{path}\\appsettings.json", optional: true, reloadOnChange: true);
        //    configurationBuilder.AddEnvironmentVariables();
        //});

        //builder.ConfigureAppConfiguration(config =>
        //{
        //    var weatherAPITestsConfiguration = ConfigurationTestSettings.GetConfigurationSettings();

        //    config.AddConfiguration(weatherAPITestsConfiguration);
        //});

        builder.ConfigureServices(services =>
        {
            var weatherAPITestsConfiguration = ConfigurationTestSettings.GetConfigurationSettings();

            services.Configure<EnvironmentOptions>(options =>
            {
                options.OpenWeatherApiKey = weatherAPITestsConfiguration.GetSection("openweather-apikey").Value;
                options.WeatherAPIApiKey = weatherAPITestsConfiguration.GetSection("weatherapi-apikey").Value;
            });
        });

        //builder.ConfigureTestServices(services =>
        //{
        //    var weatherAPITestsConfiguration = ConfigurationSettings.GetConfigurationSettings();

        //    services.Configure<OpenWeatherOptions>(weatherAPITestsConfiguration.GetSection("OpenWeather"));
        //    services.Configure<WeatherAPIOptions>(weatherAPITestsConfiguration.GetSection("WeatherAPI"));

        //    //services.Configure<WeatherAPIOptions>(
        //    //    _configuration.GetChildren().Any(x => x.Key == WeatherAPIOptions.Name) ?
        //    //    _configuration.GetSection(WeatherAPIOptions.Name) :
        //    //    weatherAPIConfiguration.GetSection(WeatherAPIOptions.Name));

        //    //services.AddSingleton(_tomorrowOptions);
        //    //services.AddSingleton(TomorrowOptionsMock.Object);
        //});
    }
}
