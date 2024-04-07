using EntityFrameworkWeatherApp.Exceptions;

namespace EntityFrameworkWeatherApp;

public static class RegisterServices
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<WeatherAPIExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        //services.AddTransient<WeatherRequestLoggingMiddleware>();

        services.AddHttpContextAccessor();

        return services;
    }
}
