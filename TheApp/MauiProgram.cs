using AiForms.Settings;
using VehicleModule;
using PeopleModule;

namespace TheApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseSettingsView()
            .UseShinyFramework(
                new DryIocContainerExtension(),
                prism => prism.CreateWindow("NavigationPage/MainPage"),
                new(
#if DEBUG
                    ErrorAlertType.FullError
#else
                    ErrorAlertType.NoLocalize
#endif
                )
            );

        builder.Configuration.AddJsonPlatformBundle();
#if DEBUG
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Logging.AddDebug();
#endif
        builder.Services.AddShinyMediator(x => x.UseMaui());
        builder.Services.AddPeopleModule();
        builder.Services.AddAnimalModule();
        
        builder.Services.AddDataAnnotationValidation();
        builder.Services.RegisterForNavigation<MainPage, MainViewModel>();
        var app = builder.Build();

        return app;
    }
}