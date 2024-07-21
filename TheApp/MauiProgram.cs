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
            )
            .UseShiny();

        builder.Configuration.AddJsonPlatformBundle();
#if DEBUG
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Logging.AddDebug();
#endif

        builder.Services.AddShinyMediator(x => x
            .AddMemoryCaching()
            .AddDataAnnotations()
            .AddPrismSupport()
            .UseMaui()
        );
        builder.AddPeopleModule();
        builder.AddVehicleModule();
        builder.AddOwnerModule();
        builder.Services.AddScoped<BaseServices>();
        
        builder.Services.RegisterForNavigation<MainPage, MainViewModel>();
        
        var app = builder.Build();

        return app;
    }
}