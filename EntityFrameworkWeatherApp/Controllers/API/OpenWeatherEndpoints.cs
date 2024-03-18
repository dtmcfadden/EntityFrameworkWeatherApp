using Asp.Versioning.Builder;
using EntityFrameworkWeatherApp.Mediator.WeatherAPIRequests;
using WeatherAPI.Models.OpenWeather;

namespace EntityFrameworkWeatherApp.Controllers.API;

internal static class OpenWeatherEndpoints
{
    public static void MapOpenWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var group = app.MapGroup("api/v{apiVersion:apiVersion}/OpenWeather").WithApiVersionSet(apiVersionSet);

        group.MapGet("geodirect", OpenWeatherRequests.GetOpenWeatherGeoDirect)
            .MapToApiVersion(1)
            .WithTags("OpenWeather Geo")
            .Produces(StatusCodes.Status200OK, typeof(List<OpenWeatherGeoDirectModel>))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(op =>
            {
                op.Summary = "Gets Geolocation information based on location name";
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

        group.MapGet("geozip", OpenWeatherRequests.GetOpenWeatherGeoZip)
            .MapToApiVersion(1)
            .WithTags("OpenWeather Geo")
            .Produces(StatusCodes.Status200OK, typeof(OpenWeatherGeoZipModel))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(op =>
            {
                op.Summary = "Gets Geolocation information based on zip/postal code";
                foreach (var item in op.Parameters)
                {
                    item.Description = item.Name switch
                    {
                        "zip" => "Use {zip/postal code},{country code}. In some cases country code can be skipped. The more specific the better.",
                        _ => null
                    }; ;
                }
                return op;
            });

        group.MapGet("directlatlong", OpenWeatherRequests.GetOpenWeatherDirectLatLong)
            .MapToApiVersion(1)
            .WithTags("OpenWeather Direct")
            .Produces(StatusCodes.Status200OK, typeof(OpenWeatherDataModel))
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

        group.MapGet("directlocation", OpenWeatherRequests.GetOpenWeatherDirectLocation)
            .MapToApiVersion(1)
            .WithTags("OpenWeather Direct")
            .Produces(StatusCodes.Status200OK, typeof(OpenWeatherDataModel))
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi(op =>
            {
                op.Summary = "Gets weather information based on location";
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
