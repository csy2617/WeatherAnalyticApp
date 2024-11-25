using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherAnalyticApp.Controllers;
using WeatherAnalyticApp.Models;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly WeatherDataContext _dbContext;
    private const string ApiKey = "9c5cfb368ced84b999b9a272d87a55bb"; // Replace with your actual API key

    public WeatherService(HttpClient httpClient, WeatherDataContext dbContext)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
    }

    public async Task<WeatherData> GetWeatherDataAsync(string city)
    {
        // Construct the URL to fetch weather data
        var requestUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric"; // `units=metric` for Celsius

        var response = await _httpClient.GetStringAsync(requestUrl);

        // Deserialize the response JSON into the WeatherData object
        var weatherResponse = JsonConvert.DeserializeObject<OpenWeatherResponse>(response);

        var weatherData = new WeatherData
        {
            City = weatherResponse.Name,
            Temperature = weatherResponse.Main.Temp,
            Humidity = weatherResponse.Main.Humidity,
            WindSpeed = weatherResponse.Wind.Speed,
            DateFetched = DateTime.Now
        };

        // Save the data to the database
        _dbContext.WeatherData.Add(weatherData);
        await _dbContext.SaveChangesAsync();

        return weatherData;
    }
}
