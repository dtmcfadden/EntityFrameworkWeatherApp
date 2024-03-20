﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WeatherAPI.Abstractions.Behaviors;
using WeatherAPI.Abstractions.Caching;
using WeatherAPI.Common;
using WeatherAPI.Entities.Validators;
using WeatherAPI.Services.Caching;


namespace WeatherAPI;
public static class RegisterServices
{
    private static readonly SocketsHttpHandler defaultSocketHandler = new()
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(5),
    };

    public static IServiceCollection AddAppWeatherAPIServices(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddSingleton<LocationStringMatches>();

        services.AddConfigureOptions(config);

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

    private static IServiceCollection AddConfigureOptions(this IServiceCollection services,
        ConfigurationManager config)
    {
        var weatherAPIConfiguration = ConfigurationSettings.GetConfigurationSettings();

        services.Configure<EnvironmentOptions>(options =>
        {
            options.OpenWeatherApiKey = weatherAPIConfiguration.GetSection("openweather-apikey").Value;
            options.WeatherAPIApiKey = weatherAPIConfiguration.GetSection("weatherapi-apikey").Value;
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
}
