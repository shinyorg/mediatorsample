namespace PeopleModule.Contracts;


public record DetailNavRequest(int PersonId) : IPrismNavigationRequest
{
    public string PageUri => Routes.Detail;
    public string? NavigationParameterName => null;
    public INavigationService? Navigator { get; set; }
};