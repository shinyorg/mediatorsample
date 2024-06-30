namespace VehicleModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public record DetailNavRequest(int VehicleId) : IPrismNavigationRequest
{
    public string PageUri => Routes.Detail;
    public string? NavigationParameterName => null;
    public INavigationService? Navigator { get; set; }
}