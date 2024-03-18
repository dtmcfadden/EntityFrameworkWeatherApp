using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkWeatherApp.Models;

public class LatLongViewModel
{
    [Required]
    [Range(-90, 90)]
    public float Latitude { get; set; }

    [Required]
    [Range(-180, 180)]
    public float Longitude { get; set; }
}
