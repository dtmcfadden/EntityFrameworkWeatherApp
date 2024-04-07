using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace EntityFrameworkWeatherApp.Infrastructure.Extentions;

public static class HttpExtentions
{
    public static IHeaderDictionary AddOrReplace(this IHeaderDictionary headers,
        KeyValuePair<string, StringValues> keyValuePair)
    {
        headers ??= new HeaderDictionary();

        headers.TryGetValue(keyValuePair.Key, out StringValues keyValue);
        if (StringValues.IsNullOrEmpty(keyValue) == false)
        {
            headers[keyValuePair.Key] = keyValuePair.Value;
        }
        else
        {
            headers.Add(keyValuePair);
        }

        return headers;
    }
}
