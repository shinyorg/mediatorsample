namespace VehicleModule.Services;


public interface IDataService
{
    Task<VehicleResult> GetById(int vehicleId);
    Task<IReadOnlyList<VehicleResult>> GetAll();
}