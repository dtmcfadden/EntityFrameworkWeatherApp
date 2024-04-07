using EntityFrameworkWeatherApp.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkWeatherApp.Infrastructure;
public static class RegisterServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DBStatus>();

        return services;
    }
}
