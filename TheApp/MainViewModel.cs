using CommunityToolkit.Mvvm.Input;
using SharedLib;
using SharedLib.Contracts;

namespace TheApp;


public partial class MainViewModel(BaseServices services, IMediator mediator) : ViewModel(services)
{
    [RelayCommand]
    Task NavToPeopleList() => 
        mediator.Send(new PeopleModule.Contracts.ListNavRequest { Navigator = this.Navigation });

    [RelayCommand]
    Task NavToVehicleList() =>
        mediator.Send(new VehicleModule.Contracts.ListNavRequest { Navigator = this.Navigation });

    [RelayCommand]
    async Task GenerateData()
    {
        await mediator.Publish(new DataGenerateEvent());
        await this.Dialogs.DisplayAlertAsync("Data Generation Complete", "Done", "Ok");
    }
}