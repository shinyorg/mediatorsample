using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedLib;

namespace VehicleModule;


public partial class ListViewModel(BaseServices services, IMediator mediator) : ViewModel(services)
{
// this.WhenAnyValueSelected(
//     x => x.SelectedVehicle,
//     async x => 
// );

    [RelayCommand]
    async Task Load()
    {
        this.List = await mediator.Request(new GetListRequest(), this.DeactiveToken);
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        this.LoadCommand.Execute(null);
    }


    [ObservableProperty] IReadOnlyList<VehicleResult> list;
    [ObservableProperty] VehicleResult? selectedVehicle;

    async partial void OnSelectedVehicleChanged(VehicleResult? value)
    {
        if (value != null)
        {
            await mediator.Send(new DetailNavRequest(value.Id) { Navigator = this.Navigation });
            this.SelectedVehicle = null;
        }
    }
}