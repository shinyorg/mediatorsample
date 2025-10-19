using AiForms.Settings;
using OwnerModule.Services.Impl;

namespace OwnerModule;


public static class Registration
{
    public static MauiAppBuilder AddOwnerModule(this MauiAppBuilder builder)
    {
        // register a custom control for this module
        builder.UseSettingsView();
        
        // register pages/viewmodels with prism
        builder.Services.RegisterForNavigation<LinkPage, LinkViewModel>();
        
        // register custom services
        builder.Services.AddSingleton<OwnerSqliteConnection>();
        builder.Services.AddSingleton<IDataService, DataService>();
        
        // handlers & middleware for mediator are registered via a module initializer
        return builder;
    }
}