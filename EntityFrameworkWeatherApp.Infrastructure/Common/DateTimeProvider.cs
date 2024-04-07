namespace EntityFrameworkWeatherApp.Infrastructure.Common;
public static class DateTimeProvider
{
    public static DateTime Now => DateTime.Now;
    public static DateTime UtcNow => DateTime.UtcNow;

    public static string SmallNow => DateTime.Now.ToString("MM/dd hh:mm:ss");
}
