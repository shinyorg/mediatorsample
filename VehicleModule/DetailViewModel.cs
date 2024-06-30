using OwnerModule.Contracts;
using SharedLib;

namespace VehicleModule;


public class DetailViewModel(
    BaseServices services,
    IDataService data,
    IMediator mediator
) : ViewModel(services)
{
    VehicleResult? vehicle;

    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        var request = parameters.Get<DetailNavRequest>()!;

        if (parameters.IsNewNavigation())
        {
            // go idea to safety this
            this.vehicle = await data.GetById(request.VehicleId, CancellationToken.None);
            this.Title = this.vehicle!.Name;

            this.Load.Execute(null);
        }
    }


    ICommand? load;
    public ICommand Load => this.load ??= ReactiveCommand.CreateFromTask(async () =>
    {
        var owners = await mediator.Request(
            new GetPeopleByVehicleRequest(this.vehicle!.Id),
            this.DeactivateToken
        );

        this.Owners = owners
            .Select(person => new ItemViewModel(
                person,
                ReactiveCommand.CreateFromTask(() =>
                    mediator.Send(new PeopleModule.Contracts.DetailNavRequest(person.Id) { Navigator = this.Navigation })
                ),
                ReactiveCommand.CreateFromTask(async () =>
                {
                    var confirm = await this.Dialogs.Confirm($"Are you sure you wish to delete '{this.vehicle!.Name}'?", "Confirm", "Yes", "No");
                    if (confirm)
                    {
                        await mediator.Send(new DeleteVehicleRequest(this.vehicle.Id), this.DeactivateToken);
                        await this.Navigation.GoBack();
                        await this.Dialogs.Snackbar($"'{this.vehicle.Name}' Successfully Deleted");
                    }
                })
            ))
            .ToList();
    });


    ICommand? add;
    public ICommand AddOwner => this.add ??= ReactiveCommand.CreateFromTask(() =>
        mediator.Send(new LinkNavRequest(null, this.vehicle!.Id) { Navigator = this.Navigation })
    );


    ICommand? delete;
    public ICommand Delete => this.delete ??= ReactiveCommand.CreateFromTask(async () =>
    {
        var confirm = await this.Dialogs.Confirm($"Are you sure you wish to delete '{this.vehicle!.Name}'?", "Confirm", "Yes", "No");
        if (confirm)
        {
            await mediator.Send(new DeleteVehicleRequest(this.vehicle!.Id), CancellationToken.None);
            await this.Navigation.GoBack();
            await this.Dialogs.Snackbar($"{this.vehicle.Name} deleted successfully!");
        }
    });

    [Reactive] public IReadOnlyList<ItemViewModel> Owners { get; private set; } = null!;
}


public class ItemViewModel(GetPeopleByVehicleResult person, ICommand viewPerson, ICommand removeOwner)
{
    public string PersonName => person.FullName;
    public ICommand ViewPerson => viewPerson;
    public ICommand RemoveOwner => removeOwner;
}