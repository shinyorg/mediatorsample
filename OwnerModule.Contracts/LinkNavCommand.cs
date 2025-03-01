namespace OwnerModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public class LinkNavCommand(int? personId, int? vehicleId) : IPrismNavigationCommand
{
    public int? PersonId => personId;
    public int? VehicleId => vehicleId;
    public string PageUri => "LinkPage";
    
    public string? PrependedNavigationUri { get; }
    public string? NavigationParameterName { get; }
    public bool? IsAnimated { get; }
    public bool IsModal { get; }
    public INavigationService? Navigator { get; set; }
};
