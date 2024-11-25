using System;

namespace WeatherAnalyticApp.Models;

public interface IWeatherService
{
    Task<WeatherData> GetWeatherDataAsync(string city);
}
