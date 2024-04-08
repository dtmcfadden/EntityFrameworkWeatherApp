using EntityFrameworkWeatherApp.Domain.Entities;
using EntityFrameworkWeatherApp.Domain.Entities.UserRequest;
using EntityFrameworkWeatherApp.Infrastructure.Common;

namespace WeatherAPI.CosmosDB.Entities;

public record WeatherRequestHistoryEntity : UserRequestEntity<Guid>
{
    public WeatherRequestHistoryEntity()
    {

    }

    public WeatherRequestHistoryEntity(HttpTrackingEntity httpTracking, string? sentParams) :
        base(httpTracking, sentParams)
    {

    }

    public override required Guid Id { get; set; }

    public DateTime CreatedDT { get; set; } = DateTimeProvider.UtcNow;
}
