using Microsoft.Extensions.Primitives;

namespace EntityFrameworkWeatherApp.Domain.Entities.UserRequest;

// https://sd.blackball.lv/articles/read/19365-how-to-get-client-ip-address-and-location-information-in-aspnet-core
// https://www.c-sharpcorner.com/article/get-ip-address-in-Asp-Net/

public record UserRequestEntity<TIdType> : BaseEntity<TIdType>
{
    public string? ConnectionID { get; set; }

    public string? RemoteIpAddress { get; set; }

    public string? Method { get; set; }

    public string? Path { get; set; }

    public string? UserAgent { get; set; }

    public string? AcceptLanguage { get; set; }

    public string? Referer { get; set; }

    public string? BlazorComponent { get; set; }

    public string? QueryString { get; set; }

    public string? SentParams { get; set; }

    public string? TraceId { get; set; }

    public Dictionary<string, string> Headers { get; set; } = [];

    public UserRequestEntity() { }

    public UserRequestEntity(HttpTrackingEntity httpTracking, string? sentParams)
    {
        BlazorComponent = httpTracking.BlazorComponent;

        var req = httpTracking.Context?.Request;

        if (httpTracking.Context is not null)
        {
            var con = httpTracking.Context.Connection;
            var headers = req?.Headers;
            // var user = httpContext.User;

            ConnectionID = con.Id;
            RemoteIpAddress = con.RemoteIpAddress.ToString();

            Method = req?.Method;
            Path = req?.Path;

            QueryString = req?.QueryString.ToString();

            if (headers is not null)
            {
                headers.TryGetValue("User-Agent", out StringValues userAgentValue);
                UserAgent = userAgentValue;

                headers.TryGetValue("Accept-Language", out StringValues acceptLanguageValue);
                AcceptLanguage = acceptLanguageValue;

                headers.TryGetValue("Referer", out StringValues refererValue);
                Referer = refererValue;

                TraceId = httpTracking.Context.TraceIdentifier;

                foreach (var key in headers.Keys)
                {
                    if (_headers.Contains(key.ToLower()) == false)
                        Headers[key] = headers[key].ToString();
                }
            }
        }
        if (sentParams is not null)
        {
            SentParams = sentParams.ToString();
        }
    }

    private readonly HashSet<string> _headers = [
        "accept",
        "accept-encoding",
        "accept-language",
        "cache-control",
        "cookie",
        "dnt",
        "host",
        "origin",
        "pragma",
        "referer",
        "user-agent",
        "sec-ch-ua",
        "sec-ch-ua-mobile",
        "sec-fetch-dest",
        "sec-fetch-mode",
        "sec-websocket-version",
        "sec-websocket-extensions"
    ];
}
