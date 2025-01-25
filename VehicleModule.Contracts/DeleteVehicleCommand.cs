namespace VehicleModule.Contracts;

public record DeleteVehicleCommand(int VehicleId) : ICommand;
