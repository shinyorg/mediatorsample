namespace VehicleModule.Contracts;

public record DeleteVehicleRequest(int VehicleId) : IRequest;
