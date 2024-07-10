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
            var request = parameters.GetRequired<DetailNavRequest>();
            this.person = await data.GetById(request.PersonId, CancellationToken.None);
            this.Title = this.person!.FullName;

            this.LoadCommand.Execute(null);
        }
    }


    [RelayCommand]
    Task AddVehicle() => mediator.Send(new LinkNavRequest(this.person!.Id, null) { Navigator = this.Navigation });

    
    [RelayCommand]
    async Task Load()
    {
        var vehicles = await mediator.Request(new GetVehiclesByPersonRequest(this.person!.Id), this.DeactiveToken);

        this.Vehicles = vehicles
            .Select(vehicle => new ItemViewModel(
                vehicle,
                ReactiveCommand.CreateFromTask(() =>
                    mediator.Send(new VehicleModule.Contracts.DetailNavRequest(vehicle.Id) { Navigator = this.Navigation })
                ),
                ReactiveCommand.CreateFromTask(async () =>
                {
                    var confirm = await this.Dialogs.DisplayAlertAsync(
                        $"Are you sure you wish to remove '{vehicle.FullName}'?", 
                        "Confirm", 
                        "Yes", 
                        "No"
                    );
                    if (confirm)
                    {
                        await mediator.Send(new LinkRequest(this.person.Id, vehicle.Id, false));
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
            await mediator.Send(new DeletePersonRequest(this.person.Id));
            await this.Navigation.GoBackAsync();
            await this.Dialogs.DisplayAlertAsync($"{this.person.FullName} was deleted successfully", "Done", "OK");
        }
    }

    [ObservableProperty] IReadOnlyList<ItemViewModel> vehicles;
}

public class ItemViewModel(GetVehiclesByPersonResult vehicle, ICommand viewVehicle, ICommand removeOwner)
{
    public string VehicleName => vehicle.FullName;
    public ICommand ViewVehicle => viewVehicle;
    public ICommand RemoveOwner => removeOwner;
}