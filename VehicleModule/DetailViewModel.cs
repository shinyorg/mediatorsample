using CommunityToolkit.Mvvm.Input;
using OwnerModule.Contracts;
using SharedLib;
using NavigationMode = Prism.Navigation.NavigationMode;

namespace VehicleModule;


public partial class DetailViewModel(
    BaseServices services,
    IDataService data,
    IMediator mediator
) : ViewModel(services)
{
    VehicleResult? vehicle;

    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);

        if (parameters.GetNavigationMode() == NavigationMode.New)
        {
            var request = parameters.GetRequired<DetailNavRequest>();
            
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
            this.DeactiveToken
        );

        this.Owners = owners
            .Select(person => new ItemViewModel(
                person,
                ReactiveCommand.CreateFromTask(() =>
                    mediator.Send(new PeopleModule.Contracts.DetailNavRequest(person.Id) { Navigator = this.Navigation })
                ),
                ReactiveCommand.CreateFromTask(async () =>
                {
                    var confirm = await this.Dialogs.DisplayAlertAsync(
                        $"Are you sure you wish to delete '{this.vehicle!.Name}'?", 
                        "Confirm", 
                        "Yes", 
                        "No"
                    );
                    if (confirm)
                    {
                        await mediator.Send(new DeleteVehicleRequest(this.vehicle.Id), this.DeactiveToken);
                        await this.Navigation.GoBackAsync();
                        await this.Dialogs.DisplayAlertAsync($"'{this.vehicle.Name}' Successfully Deleted", "Done", "Ok");
                    }
                })
            ))
            .ToList();
    });


    [RelayCommand]
    Task AddOwner() => mediator.Send(new LinkNavRequest(null, this.vehicle!.Id) { Navigator = this.Navigation });


    [RelayCommand]
    async Task Delete()
    {
        var confirm = await this.Dialogs.DisplayAlertAsync(
            $"Are you sure you wish to delete '{this.vehicle!.Name}'?", 
            "Confirm", 
            "Yes", 
            "No"
        );
        if (confirm)
        {
            await mediator.Send(new DeleteVehicleRequest(this.vehicle!.Id), CancellationToken.None);
            await this.Navigation.GoBackAsync();
            await this.Dialogs.DisplayAlertAsync($"{this.vehicle.Name} deleted successfully!", "Done", "Ok");
        }
    }

    [Reactive] public IReadOnlyList<ItemViewModel> Owners { get; private set; } = null!;
}


public class ItemViewModel(GetPeopleByVehicleResult person, ICommand viewPerson, ICommand removeOwner)
{
    public string PersonName => person.FullName;
    public ICommand ViewPerson => viewPerson;
    public ICommand RemoveOwner => removeOwner;
}