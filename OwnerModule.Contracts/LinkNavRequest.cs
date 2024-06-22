namespace OwnerModule.Contracts;

public record LinkNavRequest(INavigationService Navigator, int? PersonId, int? VehicleId) : IRequest;
