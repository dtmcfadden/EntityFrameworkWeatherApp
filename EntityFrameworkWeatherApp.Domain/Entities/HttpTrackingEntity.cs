using Microsoft.AspNetCore.Http;

namespace EntityFrameworkWeatherApp.Domain.Entities;
public record HttpTrackingEntity
{
    public HttpContext? Context { get; set; }

    public string? BlazorComponent { get; set; }
}
