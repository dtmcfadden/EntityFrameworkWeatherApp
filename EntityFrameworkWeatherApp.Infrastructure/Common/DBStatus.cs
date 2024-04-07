namespace EntityFrameworkWeatherApp.Infrastructure.Common;
public class DBStatus
{
    public Dictionary<string, bool> IsDatabaseInitialized { get; set; } = [];
}
