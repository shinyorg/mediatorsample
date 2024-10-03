using CommunityToolkit.Mvvm.ComponentModel;
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
            if (this.vehicle == null)
            {
                await this.Navigation.GoBackAsync();
                return;
            }
            this.Title = this.vehicle!.Name;
        }
        this.LoadCommand.Execute(null);
    }


    [RelayCommand]
    async Task Load()
    {
        var result = await mediator.RequestWithContext(
            new GetPeopleByVehicleRequest(this.vehicle!.Id),
            this.DeactiveToken
        );
        this.ResultsFrom = result.Context.Cache()?.Timestamp.ToString("dddd MMMM dd, h:mm:ss t") ?? "-";
        
        this.Owners = result
            .Result
            .Select(person => new ItemViewModel(
                person,
                new AsyncRelayCommand(() =>
                    mediator.Send(new PeopleModule.Contracts.DetailNavRequest(person.Id) { Navigator = this.Navigation })
                ),
                new AsyncRelayCommand(async () =>
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
    }


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

    [ObservableProperty] IReadOnlyList<ItemViewModel> owners;
    [ObservableProperty] string? resultsFrom;
}


public class ItemViewModel(GetPeopleByVehicleResult person, ICommand viewPerson, ICommand removeOwner)
{
    public string PersonName => person.FullName;
    public ICommand ViewPerson => viewPerson;
    public ICommand RemoveOwner => removeOwner;
}