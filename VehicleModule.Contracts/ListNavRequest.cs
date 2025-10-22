namespace VehicleModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public class ListNavRequest : IPrismNavigationCommand
{
    public string? PrependedNavigationUri => Routes.List;
    public string PageUri { get; }
    public string? NavigationParameterName { get; }
    public bool? IsAnimated { get; }
    public bool IsModal { get; }
    public INavigationService? Navigator { get; set; }
}
