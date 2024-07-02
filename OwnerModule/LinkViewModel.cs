using ReactiveUI;
using SharedLib;
using VehicleModule.Contracts;
using PeopleModule.Contracts;

namespace OwnerModule;


public class LinkViewModel(BaseServices services, IMediator mediator) : ViewModel(services)
{
    ICommand? add;
    public ICommand Add => this.add ??= ReactiveCommand.CreateFromTask(
        async () =>
        {
            await mediator.Send(new LinkRequest(this.SelectedPerson!.Id, this.SelectedVehicle!.Id, true));
            await this.Navigation.GoBack();
            await this.Dialogs.Snackbar("Owner Added");
        },
        this.WhenAny(
            x => x.SelectedPerson,
            x => x.SelectedVehicle,
            (p, v) => p.GetValue() != null && v.GetValue() != null
        )
    );

    [Reactive] public IReadOnlyList<PersonResult> People { get; private set; } = null!;
    [Reactive] public IReadOnlyList<VehicleResult> Vehicles { get; private set; } = null!;
    [Reactive] public VehicleResult? SelectedVehicle { get; set; }
    [Reactive] public PersonResult? SelectedPerson { get; set; }

    [Reactive] public string AddText { get; private set; } = null!;
    [Reactive] public bool IsVehicleEnabled { get; private set; }
    [Reactive] public bool IsPersonEnabled { get; private set; }
    
    
    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        if (parameters.IsNewNavigation())
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