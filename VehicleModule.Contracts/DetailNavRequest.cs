namespace VehicleModule.Contracts;

public record DetailNavRequest(INavigationService Navigator, int VehicleId) : IRequest;