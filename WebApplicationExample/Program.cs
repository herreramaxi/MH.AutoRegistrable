using WebApplicationExample;
using MH.AutoRegistrable;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoRegistrableServices(typeof(IWebApiMarker).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapGet("/weatherforecast", (IWeatherForecastService service) =>
{
    var weatherForecast = service.GetWeatherForecast();

    return weatherForecast;
});

app.Run();