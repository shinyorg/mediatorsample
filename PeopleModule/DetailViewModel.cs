using OwnerModule.Contracts;
using SharedLib;

namespace PeopleModule;


public class DetailViewModel(BaseServices services, IDataService data, IMediator mediator) : ViewModel(services)
{
    PersonResult? person;
    
    
    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);

        if (parameters.IsNewNavigation())
        {
            var request = parameters.Get<DetailNavRequest>()!;
            this.person = await data.GetById(request.PersonId, CancellationToken.None);
            this.Title = this.person!.FullName;

            this.Load.Execute(null);
        }
    }
    

    ICommand? add;
    public ICommand AddVehicle => this.add ??= ReactiveCommand.CreateFromTask(() =>
        mediator.Send(new LinkNavRequest(this.Navigation, this.person!.Id, null))
    );
    

    ICommand? load;
    public ICommand Load => this.load ??= ReactiveCommand.CreateFromTask(async () =>
    {
        var vehicles = await mediator.Request(new GetVehiclesByPersonRequest(this.person!.Id), this.DeactivateToken);

        this.Vehicles = vehicles
            .Select(vehicle => new ItemViewModel(
                vehicle,
                ReactiveCommand.CreateFromTask(() =>
                    mediator.Send(new VehicleModule.Contracts.DetailNavRequest(this.Navigation, vehicle.Id))
                ),
                ReactiveCommand.CreateFromTask(async () =>
                {
                    var confirm = await this.Dialogs.Confirm($"Are you sure you wish to remove '{vehicle.FullName}'?", "Confirm", "Yes", "No");
                    if (confirm)
                    {
                        await mediator.Send(new LinkRequest(this.person.Id, vehicle.Id, false));
                        this.Load.Execute(null);
                        await this.Dialogs.Confirm($"Removed '{vehicle.FullName}' Successfully");
                    }
                })
            ))
            .ToList();
    });


    ICommand? delete;
    public ICommand Delete => this.delete ??= ReactiveCommand.CreateFromTask(async () =>
    {
        var confirm = await this.Dialogs.Confirm($"Do you wish to delete '{this.person!.FullName}'?", "Confirm", "Yes", "No");
        if (confirm)
        {
            await mediator.Send(new DeletePersonRequest(this.person.Id));
            await this.Navigation.GoBack();
            await this.Dialogs.Snackbar($"{this.person.FullName} was deleted successfully");
        }
    });

    [Reactive] public IReadOnlyList<ItemViewModel> Vehicles { get; private set; } = null!;
}

public class ItemViewModel(GetVehiclesByPersonResult vehicle, ICommand viewVehicle, ICommand removeOwner)
{
    public string VehicleName => vehicle.FullName;
    public ICommand ViewVehicle => viewVehicle;
    public ICommand RemoveOwner => removeOwner;
}