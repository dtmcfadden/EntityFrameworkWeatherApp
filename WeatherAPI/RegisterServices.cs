using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WeatherAPI.Abstractions.Behaviors;
using WeatherAPI.Abstractions.Caching;
using WeatherAPI.Common;
using WeatherAPI.CosmosDB.IRepository;
using WeatherAPI.CosmosDB.Repository;
using WeatherAPI.CosmosDB.UnitOfWork;
using WeatherAPI.Entities.Validators;
using WeatherAPI.Repositories;
using WeatherAPI.Services.Caching;

namespace WeatherAPI;
public static class RegisterServices
{
    private static readonly IConfigurationRoot weatherAPIConfiguration =
        ConfigurationSettings.GetConfigurationSettings();

    private static readonly SocketsHttpHandler defaultSocketHandler = new()
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(5),
    };

    public static IServiceCollection AddAppWeatherAPIServices(this IServiceCollection services)
    {
        services.AddSingleton<LocationStringMatches>();

        services.AddConfigureOptions();

        services.AddDatabaseServices();

        services.AddWeatherAPIValidators();

        services.AddHTTPServices();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
        });

        services.AddMemoryCache(config =>
        {
            config.SizeLimit = (1024 * 1);
        });
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }

    private static IServiceCollection AddWeatherAPIValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LatLongEntityValidator>();
        services.AddValidatorsFromAssemblyContaining<LocationEntityValidator>();

        return services;
    }

    private static IServiceCollection AddConfigureOptions(this IServiceCollection services)
    {
        services.Configure<EnvironmentOptions>(options =>
        {
            if (weatherAPIConfiguration != null)
            {
                var owSection = weatherAPIConfiguration.GetSection("openweather-apikey");
                if (owSection.Value != null)
                    options.OpenWeatherApiKey = owSection.Value;

                var waSection = weatherAPIConfiguration.GetSection("weatherapi-apikey");
                if (waSection.Value != null)
                    options.WeatherAPIApiKey = waSection.Value;

                var weatherConnectionString = weatherAPIConfiguration.GetSection("weather-connectionstring");
                if (weatherConnectionString.Value != null)
                    options.WeatherConnectionString = weatherConnectionString.Value;
            }
        });

        return services;
    }

    private static IServiceCollection AddHTTPServices(this IServiceCollection services)
    {
        services.AddHttpClient<IOpenWeatherHTTPService, OpenWeatherHTTPService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");
        })
            .ConfigurePrimaryHttpMessageHandler(() => defaultSocketHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        services.AddHttpClient<IWeatherAPIHTTPService, WeatherAPIHTTPService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri("http://api.weatherapi.com/v1/");
        })
            .ConfigurePrimaryHttpMessageHandler(() => defaultSocketHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        services.AddHttpClient<GeneralHTTPService>()
            .ConfigurePrimaryHttpMessageHandler(() => defaultSocketHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        return services;
    }

    private static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {

        var dbCS = weatherAPIConfiguration.GetSection("weather-connectionstring");
        if (dbCS.Value is null)
            throw new CosmosDBDatabaseNotFoundException("weather-connectionstring");

        services.AddDbContext<WeatherDBContext>(options =>
            options.UseCosmos(dbCS.Value, "weather"));

        services.AddScoped<IUnitOfWork<WeatherDBContext>, UnitOfWork>();
        services.AddScoped<IWeatherRequestHistoryRepository, WeatherRequestHistoryRepository>();

        return services;
    }
}
