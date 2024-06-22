using SharedLib;

namespace VehicleModule;


public class DetailViewModel(
    BaseServices services, 
    IDataService data,
    IMediator mediator
) : ViewModel(services)
{

    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        var request = parameters.Get<DetailNavRequest>();
        var vehicle = await data.GetById(request.VehicleId);
        this.Title = vehicle.Name;
    }
}