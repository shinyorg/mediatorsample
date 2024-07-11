using System.Reactive.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedLib;
using VehicleModule.Contracts;
using PeopleModule.Contracts;
using NavigationMode = Prism.Navigation.NavigationMode;

namespace OwnerModule;


public partial class LinkViewModel(BaseServices services, IMediator mediator) : ViewModel(services)
{
    [RelayCommand(CanExecute = nameof(CanExecuteCmd))]
    async Task Add()
    {
        await mediator.Send(new LinkRequest(this.SelectedPerson!.Id, this.SelectedVehicle!.Id, true));
        await this.Navigation.GoBackAsync();
        await this.Dialogs.DisplayAlertAsync("Owner Added", "Done", "Ok");
    }

    bool CanExecuteCmd() => this.SelectedPerson != null && this.SelectedVehicle != null;
    

    [ObservableProperty] IReadOnlyList<PersonResult> people;
    [ObservableProperty] IReadOnlyList<VehicleResult> vehicles;
    [ObservableProperty] VehicleResult? selectedVehicle;
    [ObservableProperty] PersonResult? selectedPerson;
    [ObservableProperty] string addText;
    [ObservableProperty] bool isVehicleEnabled;
    [ObservableProperty] bool isPersonEnabled;


    public override void OnAppearing()
    {
        base.OnAppearing();
        this.WhenAnyProperty()
            .Subscribe(_ => this.AddCommand.NotifyCanExecuteChanged());
        // .DisposedBy();

    }

    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        if (parameters.GetNavigationMode() == NavigationMode.New)
        {
            var request = parameters.GetRequired<LinkNavRequest>();
            await Task.WhenAll(this.BindPeople(), this.BindVehicles());
            
            if (request.PersonId == null)
            {
                this.IsPersonEnabled = true;
                this.AddText = "Add Owner";
                this.SelectedVehicle = this.Vehicles.FirstOrDefault(x => x.Id == request.VehicleId);
            }
            else
            {
                this.IsVehicleEnabled = true;
                this.AddText = "Add Vehicle";
                this.SelectedPerson = this.People.FirstOrDefault(x => x.Id == request.PersonId);
            }
        }
    }


    async Task BindPeople()
        => this.People = await mediator.Request(new PeopleModule.Contracts.GetListRequest());

    async Task BindVehicles()
        => this.Vehicles = await mediator.Request(new VehicleModule.Contracts.GetListRequest());
}