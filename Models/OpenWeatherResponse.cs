using System;

namespace WeatherAnalyticApp.Models;

public class OpenWeatherResponse
{
    public string Name { get; set; } // City name
    public Main Main { get; set; } // Main weather data (temperature, humidity)
    public Wind Wind { get; set; } // Wind data (wind speed)
}

public class Main
{
    public double Temp { get; set; } // Temperature
    public int Humidity { get; set; } // Humidity
}

public class Wind
{
    public double Speed { get; set; } // Wind speed
}

