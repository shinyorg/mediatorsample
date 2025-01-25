using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedLib;
using VehicleModule.Contracts;
using PeopleModule.Contracts;
using NavigationMode = Prism.Navigation.NavigationMode;

namespace OwnerModule;


public partial class LinkViewModel(BaseServices services, IMediator mediator) : ViewModel(services)
{
    [RelayCommand]
    async Task Add()
    {
        try
        {
            this.PersonErrorMessage = null;
            this.VehicleErrorMessage = null;
            
            await mediator.Send(new LinkCommand
            {
                PersonId = this.SelectedPerson?.Id,
                VehicleId = this.SelectedVehicle?.Id,
                Link = true
            });
            await this.Navigation.GoBackAsync();
            await this.Dialogs.DisplayAlertAsync("Owner Added", "Done", "Ok");
        }
        catch (ValidateException ex)
        {
            if (ex.Result.Errors.ContainsKey(nameof(LinkCommand.VehicleId)))
                this.VehicleErrorMessage = ex.Result.Errors[nameof(LinkCommand.VehicleId)].FirstOrDefault();
            
            if (ex.Result.Errors.ContainsKey(nameof(LinkCommand.PersonId)))
                this.PersonErrorMessage = ex.Result.Errors[nameof(LinkCommand.PersonId)].FirstOrDefault();
        }
    }
    

    [ObservableProperty] IReadOnlyList<PersonResult> people;
    [ObservableProperty] IReadOnlyList<VehicleResult> vehicles;
    [ObservableProperty] VehicleResult? selectedVehicle;
    [ObservableProperty] PersonResult? selectedPerson;
    [ObservableProperty] string? addText;
    [ObservableProperty] bool isVehicleEnabled;
    [ObservableProperty] bool isPersonEnabled;
    [ObservableProperty] string? personErrorMessage;
    [ObservableProperty] string? vehicleErrorMessage;
    

    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        if (parameters.GetNavigationMode() == NavigationMode.New)
        {
            var request = parameters.GetRequired<LinkNavCommand>();
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