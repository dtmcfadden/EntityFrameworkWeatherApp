using WeatherAPI.Requests.Commands.Weather;

namespace EntityFrameworkWeatherApp.Middleware;

public class WeatherRequestLoggingMiddleware : IMiddleware
{
    private readonly ISender _sender;
    public WeatherRequestLoggingMiddleware(ISender sender)
    {
        _sender = sender;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var path = context.Request.Path;
        StringComparison comparison = StringComparison.OrdinalIgnoreCase;

        if (path.StartsWithSegments("/api", comparison) == true)
        {
            _ = _sender.Send(new AddWeatherEnpointCallCommand(new() { Context = context }, ""));
        }

        await next(context);
    }
}
