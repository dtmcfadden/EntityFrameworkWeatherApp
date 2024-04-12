using Asp.Versioning.Builder;
using EntityFrameworkWeatherApp.Mediator.WeatherAPI.CombinedWeather;
using WeatherAPI.Models.CombinedWeather;

namespace EntityFrameworkWeatherApp.Controllers.API;

internal static class CombinedWeatherEndpoints
{
    public static void MapCombinedWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("api/v{apiVersion:apiVersion}/CombinedWeather").WithApiVersionSet(apiVersionSet);

        group.MapGet("combinedlatlong", CombinedWeatherRequests.GetCombinedWeatherLatLong)
            .MapToApiVersion(1)
            .WithTags("CombinedWeather Weather")
            .Produces(StatusCodes.Status200OK, typeof(CombinedWeatherDataModel))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(op =>
            {
                op.Summary = "Gets weather information based on latitude and longitude from weather API on rotation";
                foreach (var item in op.Parameters)
                {
                    item.Description = item.Name switch
                    {
                        "latitude" => "Use {latitude}. Between -90 and 90",
                        "longitude" => "Use {longitude}. Between -180 and 180",
                        _ => null
                    }; ;
                }
                return op;
            });

        group.MapGet("combinedlocation", CombinedWeatherRequests.GetCombinedWeatherLocation)
            .MapToApiVersion(1)
            .WithTags("CombinedWeather Weather")
            .Produces(StatusCodes.Status200OK, typeof(CombinedWeatherDataModel))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(op =>
            {
                op.Summary = "Gets weather information based on location from weather API on rotation";
                foreach (var item in op.Parameters)
                {
                    item.Description = item.Name switch
                    {
                        "location" => "Use {city name},{state code},{country code} or {zip/postal code},{country code}. In some cases country code can be skipped. The more specific the better.",
                        _ => null
                    }; ;
                }
                return op;
            });
    }
}
