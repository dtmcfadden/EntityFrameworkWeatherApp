using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkWeatherApp.Models;

public class LocationNameViewModel
{
    [Required]
    [StringLength(50)]
    public string Location { get; set; }
}
