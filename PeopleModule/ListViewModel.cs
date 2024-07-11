using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedLib;
using DetailNavRequest = VehicleModule.Contracts.DetailNavRequest;

namespace PeopleModule;


public partial class ListViewModel(BaseServices services, IMediator mediator) : ViewModel(services)
{
    [RelayCommand]
    async Task Load()
    {
        this.IsBusy = true;
        this.List = await mediator.Request(new GetListRequest(), this.DeactiveToken);
        this.IsBusy = false;
    }
    
    [ObservableProperty] IReadOnlyList<PersonResult> list;
    [ObservableProperty] PersonResult? selectedPerson;
    async partial void OnSelectedPersonChanged(PersonResult? value)
    {
        if (value != null)
        {
            await mediator.Send(new DetailNavRequest(value.Id) { Navigator = this.Navigation });
            this.SelectedPerson = null;
        }
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        this.LoadCommand.Execute(null);
    }
}