namespace VehicleModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
// public string PageUri => Routes.Detail;
public class DetailNavCommand(int vehicleId) : IPrismNavigationCommand
{
    public int VehicleId => vehicleId;
    public string? PrependedNavigationUri { get; }
    public string PageUri => Routes.Detail;
    public string? NavigationParameterName { get; }
    public bool? IsAnimated { get; }
    public bool IsModal { get; }
    public INavigationService? Navigator { get; set; }
}