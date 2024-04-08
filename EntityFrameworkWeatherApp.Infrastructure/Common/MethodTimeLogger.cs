using System.Diagnostics;
using System.Reflection;

namespace EntityFrameworkWeatherApp.Infrastructure.Common;
public static class MethodTimeLogger
{
    //public static ILogger Logger;
    public static void Log(
        MethodBase methodBase,
        TimeSpan elapsed,
        string message)
    {
        //Do some logging here
        Trace.WriteLine(string.Format("MethodTime:\t{0}\t{1:D4}\t{2}\t{3}\t{4}\t{5}",
            DateTimeProvider.SmallNow,
            elapsed.Microseconds,
            methodBase.DeclaringType!.Name,
            methodBase.Name,
            elapsed,
            message));
    }
}
