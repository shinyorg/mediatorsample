namespace VehicleModule.Contracts;

public record ListNavRequest(INavigationService Navigator) : IRequest;
