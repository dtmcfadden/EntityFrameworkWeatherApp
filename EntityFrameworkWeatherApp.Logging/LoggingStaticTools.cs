using Newtonsoft.Json;

namespace EntityFrameworkWeatherApp.Logging;

public static class LoggingStaticTools
{
    public static string ObjectToString(object request)
    {
        if (request == null) return "";
        return JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        });
    }
}
