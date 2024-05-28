# MH.AutoRegistrable

`MH.AutoRegistrable` is a .NET library that provides an easy way to automatically register services into the dependency injection container by scanning assemblies for types marked with a custom attribute.

## Installation

You can install the `MH.AutoRegistrable` package via NuGet Package Manager or .NET CLI.

### NuGet Package Manager
```powershell
PM> Install-Package MH.AutoRegistrable
```

### .NET CLI
```powershell
dotnet add package MH.AutoRegistrable
```

## Usage

To use the MH.AutoRegistrable library, follow these steps:

1. Mark the services you want to auto-register with the `AutoRegistrableAttribute`:
```csharp    
    [AutoRegistrable(ServiceLifetime.Scoped, typeof(IWeatherForecastService))]
    public class WeatherForecastService : IWeatherForecastService
    ...
```
2. Register the services in `Startup.cs` or `Program.cs`**:
   Call the `AddAutoRegistrableServices` extension method in your service configuration.

    ```csharp
    using Microsoft.Extensions.DependencyInjection;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddAutoRegistrableServices(typeof(IWeatherForecastService).Assembly);

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.MapGet("/weatherforecast", (IWeatherForecastService service) =>
    {
        var weatherForecast = service.GetWeatherForecast();
        return weatherForecast;
    });

    app.Run();
    ```
