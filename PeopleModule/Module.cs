using PeopleModule.Services.Impl;

namespace PeopleModule;


public static class Registration
{
    public static MauiAppBuilder AddPeopleModule(this MauiAppBuilder builder)
    {
        // register pages/viewmodels with prism
        builder.Services.RegisterForNavigation<DetailPage, DetailViewModel>(Routes.Detail);
        builder.Services.RegisterForNavigation<ListPage, ListViewModel>(Routes.List);

        // register custom services
        builder.Services.AddSingleton<PeopleSqliteConnection>();
        builder.Services.AddSingleton<IDataService, DataService>();

        // register source generated handlers
        builder.Services.AddDiscoveredMediatorHandlersFromPeopleModule();
        return builder;
    }
}