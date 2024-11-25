using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WeatherAnalyticApp.Controllers;
using WeatherAnalyticApp.Models;

public class WeatherController : Controller
{
    private readonly IWeatherService _weatherService;
    private readonly WeatherDataContext _dbContext;

    public WeatherController(IWeatherService weatherService, WeatherDataContext dbContext)
    {
        _weatherService = weatherService;
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index(string city = "London")
    {
        // Fetch the weather data from the database
        var weatherData = await _dbContext.WeatherData
            .Where(w => w.City == city)
            .OrderByDescending(w => w.DateFetched)
            .FirstOrDefaultAsync();

        // If not found, fetch from OpenWeatherMap and save to the database
        if (weatherData == null)
        {
            weatherData = await _weatherService.GetWeatherDataAsync(city);
        }

        return View(weatherData);
    }

[HttpGet]
    public async Task<IActionResult> Chart()
    {
        // Fetch all weather data for all cities
        var weatherData = await _dbContext.WeatherData
            .OrderBy(w => w.DateFetched)
            .ToListAsync();

        // Group the data by city
        var chartData = weatherData
            .GroupBy(w => w.City)
            .Select(g => new
            {
                City = g.Key,
                Temperatures = g.Select(w => new
                {
                    Date = w.DateFetched.ToString("yyyy-MM-dd HH:mm:ss"),
                    Temperature = w.Temperature
                }).ToList()
            })
            .ToList();

        // Return the data as JSON to the view for chart rendering
        return Json(chartData);
    }
}
