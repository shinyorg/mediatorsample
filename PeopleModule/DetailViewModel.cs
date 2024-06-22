using SharedLib;

namespace PeopleModule;


public class DetailViewModel(BaseServices services, IDataService data) : ViewModel(services)
{
    public override async void OnNavigatedTo(INavigationParameters parameters)
    {
        base.OnNavigatedTo(parameters);
        var request = parameters.Get<DetailNavRequest>();
        var person = await data.GetById(request.PersonId);
        this.Title = person.FullName;
    }
}