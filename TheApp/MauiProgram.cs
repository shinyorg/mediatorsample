using Microsoft.Extensions.Configuration;
using OwnerModule;
using VehicleModule;
using PeopleModule;
using SharedLib;

namespace TheApp;


public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UsePrism(
                new DryIocContainerExtension(),
                prism => prism.CreateWindow("NavigationPage/MainPage")
            );

        builder.Configuration.AddJsonPlatformBundle();
#if DEBUG
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Logging.AddDebug();
#endif



        var servs = builder.Services.Where(x => x is { ServiceType: not null, ImplementationType: not null }).ToList();
        foreach (var service in servs)
        {
            Console.WriteLine($"{service.ServiceType.FullName} - {service.Lifetime} - {service.ImplementationType!.FullName}");
        }
        builder.AddPeopleModule();
        builder.AddVehicleModule();
        builder.AddOwnerModule();
        
        // module initializers don't run until they're actual library is hit
        builder.Services.AddShinyMediator(x => x
            .AddRegistry()
            .AddMauiPersistentCache()
            .AddDataAnnotations()
            .AddPrismSupport()
            .UseMaui()
        );
        builder.Services.AddScoped<BaseServices>();
        builder.Services.RegisterForNavigation<MainPage, MainViewModel>();
        
        var app = builder.Build();

        return app;
    }
}