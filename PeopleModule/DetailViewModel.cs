using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OwnerModule.Contracts;
using SharedLib;
using NavigationMode = Prism.Navigation.NavigationMode;

namespace PeopleModule;


public partial class DetailViewModel(BaseServices services, IDataService data, IMediator mediator) : ViewModel(services)
{
    PersonResult? person;


    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);

        if (parameters.GetNavigationMode() == NavigationMode.New)
        {
            var request = parameters.GetRequired<DetailNavCommand>();
            this.person = await data.GetById(request.PersonId, CancellationToken.None);
            if (this.person == null)
            {
                await this.Navigation.GoBackAsync();
                return;
            }
            this.Title = this.person!.FullName;
        }
        this.LoadCommand.Execute(null);
    }


    [RelayCommand]
    Task AddVehicle() => mediator.Send(new LinkNavCommand(this.person!.Id, null) { Navigator = this.Navigation });

    
    [RelayCommand]
    async Task Load()
    {
        var context = await mediator.RequestWithContext(
            new GetVehiclesByPersonRequest(this.person!.Id), 
            this.DeactiveToken
        );
        this.ResultsFrom = context.Context.Cache()?.Timestamp.ToString("dddd MMMM dd, h:mm:ss t") ?? "-";
        
        this.Vehicles = context
            .Result
            .Select(vehicle => new ItemViewModel(
                vehicle,
                new AsyncRelayCommand(() =>
                    mediator.Send(new VehicleModule.Contracts.DetailNavCommand(vehicle.Id) { Navigator = this.Navigation })
                ),
                new AsyncRelayCommand(async () =>
                {
                    var confirm = await this.Dialogs.DisplayAlertAsync(
                        $"Are you sure you wish to remove '{vehicle.FullName}'?", 
                        "Confirm", 
                        "Yes", 
                        "No"
                    );
                    if (confirm)
                    {
                        await mediator.Send(new LinkCommand
                        {
                            PersonId = this.person?.Id, 
                            VehicleId = vehicle.Id, 
                            Link = false
                        });
                        this.LoadCommand.Execute(null);
                        await this.Dialogs.DisplayAlertAsync($"Removed '{vehicle.FullName}' Successfully", "Done", "Ok");
                    }
                })
            ))
            .ToList();
    }


    [RelayCommand]
    async Task Delete()
    {
        var confirm = await this.Dialogs.DisplayAlertAsync($"Do you wish to delete '{this.person!.FullName}'?", "Confirm", "Yes", "No");
        if (confirm)
        {
            await mediator.Send(new DeletePersonCommand(this.person.Id));
            await this.Navigation.GoBackAsync();
            await this.Dialogs.DisplayAlertAsync($"{this.person.FullName} was deleted successfully", "Done", "OK");
        }
    }

    [ObservableProperty] IReadOnlyList<ItemViewModel> vehicles;
    [ObservableProperty] string? resultsFrom;
}

public class ItemViewModel(GetVehiclesByPersonResult vehicle, ICommand viewVehicle, ICommand removeOwner)
{
    public string VehicleName => vehicle.FullName;
    public ICommand ViewVehicle => viewVehicle;
    public ICommand RemoveOwner => removeOwner;
}