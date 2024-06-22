using VehicleModule.Services.Impl;

namespace VehicleModule;


public static class Registration
{
    public static MauiAppBuilder AddVehicleModule(this MauiAppBuilder builder)
    {
        // register pages/viewmodels with prism
        builder.Services.RegisterForNavigation<DetailPage, DetailViewModel>(Routes.Detail);
        builder.Services.RegisterForNavigation<ListPage, ListViewModel>(Routes.List);
        
        // register custom services
        builder.Services.AddSingleton<VehicleSqliteConnection>();
        builder.Services.AddSingleton<IDataService, DataService>();
        
        // register source generated handlers
        builder.Services.AddDiscoveredMediatorHandlersFromVehicleModule();
        return builder;
    }
}