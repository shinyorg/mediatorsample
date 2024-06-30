namespace PeopleModule.Contracts;


// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public record DetailNavRequest(int PersonId) : IPrismNavigationRequest
{
    public string PageUri => Routes.Detail;
    public string? NavigationParameterName => null;
    public INavigationService? Navigator { get; set; }
};