namespace VehicleModule.Contracts;

public record DeleteVehicleEvent(int VehicleId, string Manufacturer, string Model) : IEvent;