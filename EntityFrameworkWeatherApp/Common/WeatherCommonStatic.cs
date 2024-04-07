using EntityFrameworkWeatherApp.Domain.Entities;
using System.Text.Json;
using WeatherAPI.Requests.Commands.Weather;
using ISender = MediatR.ISender;

namespace EntityFrameworkWeatherApp.Common;

public static class WeatherCommonStatic
{
    public static void AddWeatherEnpointHistory(
        ISender sender,
        HttpTrackingEntity httpTracking,
        params object[]? sentParams)
    {
        var serializeParams = "";
        try
        {
            if (sentParams is not null)
                serializeParams = JsonSerializer.Serialize(sentParams);

        }
        catch (Exception)
        {

        }

        _ = sender.Send(new AddWeatherEnpointCallCommand(httpTracking, serializeParams));
    }

    //public static IHeaderDictionary AddReplace(this )
}
