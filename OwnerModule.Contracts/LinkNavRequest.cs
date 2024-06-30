namespace OwnerModule.Contracts;

public record LinkNavRequest(int? PersonId, int? VehicleId) : IPrismNavigationRequest
{
    public string PageUri => "LinkPage";

    public string? NavigationParameterName => null!;
    public INavigationService? Navigator { get; set; }
};
