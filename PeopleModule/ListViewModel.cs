using ReactiveUI;

namespace PeopleModule;


public class ListViewModel : ViewModel
{
    public ListViewModel(BaseServices services, IMediator mediator) : base(services)
    {
        this.Load = ReactiveCommand.CreateFromTask(async () =>
            this.List = await mediator.Request(new GetListRequest())
        );
        this.BindBusyCommand(this.Load);
        
        this.WhenAnyValueSelected(
            x => x.SelectedPerson,
            async x => await mediator.Send(new DetailNavRequest(this.Navigation, x.Id))
        );        
    }
    
    public ICommand Load { get; }
    [Reactive] public IReadOnlyList<PersonResult> List { get; private set; }
    [Reactive] public PersonResult? SelectedPerson { get; set; }

    
    public override void OnAppearing()
    {
        base.OnAppearing();
        this.Load.Execute(null);
    }
}