namespace VehicleModule;


public class ListViewModel : ViewModel
{
    public ListViewModel(BaseServices services, IMediator mediator) : base(services)
    {
        this.Load = ReactiveCommand.CreateFromTask(async () =>
            this.List = await mediator.Request(new GetListRequest())
        );
        this.BindBusyCommand(this.Load);

        this.WhenAnyValueSelected(
            x => x.SelectedVehicle,
            async x => await mediator.Send(new DetailNavRequest(x.Id) { Navigator = this.Navigation })
        );
    }


    public override void OnAppearing()
    {
        base.OnAppearing();
        this.Load.Execute(null);
    }


    public ICommand Load { get; }
    [Reactive] public IReadOnlyList<VehicleResult> List { get; private set; }
    [Reactive] public VehicleResult? SelectedVehicle { get; set; }
}