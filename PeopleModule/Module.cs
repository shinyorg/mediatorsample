using PeopleModule.Services.Impl;

namespace PeopleModule;


public static class Registration
{
    public static IServiceCollection AddPeopleModule(this IServiceCollection services)
    {
        services.RegisterForNavigation<DetailPage, DetailViewModel>(Routes.Detail);
        services.RegisterForNavigation<ListPage, ListViewModel>(Routes.List);
        
        services.AddSingleton<PeopleSqliteConnection>();
        services.AddSingleton<IDataService, DataService>();
        
        services.AddDiscoveredMediatorHandlersFromPeopleModule(); // source gen'd
        return services;
    }
}