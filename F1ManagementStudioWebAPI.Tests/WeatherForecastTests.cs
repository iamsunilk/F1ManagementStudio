using System;
using F1ManagementStudioWebAPI;
using Xunit;

public class WeatherForecastTests
{
    [Fact]
    public void WeatherForecast_Properties_ShouldBeSetCorrectly()
    {
        var forecast = new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = 25,
            Summary = "Warm"
        };

        Assert.Equal(25, forecast.TemperatureC);
        Assert.Equal("Warm", forecast.Summary);
    }
}