
namespace VehicleModule.Contracts;

public record DetailNavRequest(int VehicleId) : IPrismNavigationRequest
{
    public string PageUri => Routes.Detail;
    public string? NavigationParameterName => null;
    public INavigationService? Navigator { get; set; }
}