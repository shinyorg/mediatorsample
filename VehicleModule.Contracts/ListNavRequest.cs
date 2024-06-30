namespace VehicleModule.Contracts;


public record ListNavRequest : IPrismNavigationRequest
{
    public string PageUri => Routes.List;
    public string? NavigationParameterName => null;
    public INavigationService? Navigator { get; set; }
}
