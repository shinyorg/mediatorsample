using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedLib;

namespace PeopleModule;


public partial class ListViewModel(BaseServices services, IMediator mediator) : ViewModel(services)
{

//     this.WhenAnyValueSelected(
//         x => x.SelectedPerson,
//         async x => await mediator.Send(new DetailNavRequest(x.Id) { Navigator = this.Navigation })
//     );

    [RelayCommand]
    async Task Load()
    {
        this.IsBusy = true;
        this.List = await mediator.Request(new GetListRequest(), this.DeactiveToken);
        this.IsBusy = false;
    }

    [ObservableProperty] IReadOnlyList<PersonResult> list;
    [ObservableProperty] PersonResult? selectedPerson;


    public override void OnAppearing()
    {
        base.OnAppearing();
        this.LoadCommand.Execute(null);
    }
}