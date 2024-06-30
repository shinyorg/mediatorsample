namespace VehicleModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public record ListNavRequest : IPrismNavigationRequest
{
    public string PageUri => Routes.List;
    public string? NavigationParameterName => null;
    public INavigationService? Navigator { get; set; }
}
