using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkWeatherApp.Controllers;
public class WeatherController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
