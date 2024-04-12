using Asp.Versioning.Builder;
using EntityFrameworkWeatherApp.Mediator.WeatherAPI.WeatherAPI;
using WeatherAPI.Models.WeatherAPI;

namespace EntityFrameworkWeatherApp.Controllers.API;

public static class WeatherAPIEndpoints
{
    public static void MapWeatherAPIEndpoints(this IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("api/v{apiVersion:apiVersion}/WeatherAPI").WithApiVersionSet(apiVersionSet);

        group.MapGet("weatherlatlong", WeatherAPIRequests.GetWeatherAPIWeatherLatLong)
            .MapToApiVersion(1)
            .WithTags("WeatherAPI Weather")
            .Produces(StatusCodes.Status200OK, typeof(WeatherAPICurrentModel))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(op =>
            {
                op.Summary = "Gets weather information based on latitude and longitude";
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

        group.MapGet("weatherlocation", WeatherAPIRequests.GetWeatherAPIWeatherByLocationName)
            .MapToApiVersion(1)
            .WithTags("WeatherAPI Weather")
            .Produces(StatusCodes.Status200OK, typeof(WeatherAPICurrentModel))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(op =>
            {
                op.Summary = "Gets weather information based on location name";
                foreach (var item in op.Parameters)
                {
                    item.Description = item.Name switch
                    {
                        "location" => "Use {city name},{state code},{country code}. In some cases partial data can be used. The more specific the better.",
                        _ => null
                    }; ;
                }
                return op;
            });
    }
}
