namespace OwnerModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public record LinkNavRequest(int? PersonId, int? VehicleId) : IPrismNavigationRequest
{
    public string PageUri => "LinkPage";

    public string? NavigationParameterName => null!;
    public INavigationService? Navigator { get; set; }
};
