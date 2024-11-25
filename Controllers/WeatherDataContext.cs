using System;
using Microsoft.EntityFrameworkCore;
using WeatherAnalyticApp.Models;

namespace WeatherAnalyticApp.Controllers;

public class WeatherDataContext: DbContext
{
     public DbSet<WeatherData> WeatherData { get; set; }

    public WeatherDataContext(DbContextOptions<WeatherDataContext> options)
        : base(options)
    { }
}
