using SharedLib;

namespace OwnerModule;


public class LinkViewModel : ViewModel
{
    public LinkViewModel(BaseServices services, IMediator mediator) : base(services)
    {
        
    }

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        var request = parameters.Get<LinkNavRequest>();
        
    }
}