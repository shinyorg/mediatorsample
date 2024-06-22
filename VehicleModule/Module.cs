using AnimalModule;
using VehicleModule.Services.Impl;

namespace VehicleModule;


public static class Registration
{
    public static IServiceCollection AddAnimalModule(this IServiceCollection services)
    {
        services.RegisterForNavigation<DetailPage, DetailViewModel>(Routes.Detail);
        services.RegisterForNavigation<ListPage, ListViewModel>(Routes.List);
        
        services.AddSingleton<VehicleSqliteConnection>();
        services.AddSingleton<IDataService, DataService>();
        
        services.AddDiscoveredMediatorHandlersFromVehicleModule(); // source gen'd
        return services;
    }
}