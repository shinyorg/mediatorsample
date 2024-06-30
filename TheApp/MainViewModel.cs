using SharedLib.Contracts;

namespace TheApp;


public class MainViewModel(BaseServices services, IMediator mediator) : ViewModel(services)
{
    ICommand? navPeople;
    public ICommand NavToPeopleList => this.navPeople ??= ReactiveCommand.CreateFromTask(
        () => mediator.Send(new PeopleModule.Contracts.ListNavRequest { Navigator = this.Navigation })
    );

    ICommand? navVehicle;
    public ICommand NavToVehicleList => this.navVehicle ??= ReactiveCommand.CreateFromTask(
        () => mediator.Send(new VehicleModule.Contracts.ListNavRequest { Navigator = this.Navigation })
    );

    ICommand? genData;
    public ICommand GenerateData => this.genData ??= ReactiveCommand.CreateFromTask(async () =>
    {
        await mediator.Publish(new DataGenerateEvent());
        await this.Dialogs.Alert("Data Generation Complete", "Done");
    });
}