namespace PeopleModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public class ListNavCommand : IPrismNavigationCommand
{
    public string? PrependedNavigationUri { get; }
    public string PageUri => Routes.List;
    public string? NavigationParameterName { get; }
    public bool? IsAnimated { get; }
    public bool IsModal { get; }
    public INavigationService? Navigator { get; set; }
};
