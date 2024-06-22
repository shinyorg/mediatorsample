namespace VehicleModule.Services;


public interface IDataService
{
    Task Delete(int vehicleId, CancellationToken cancellationToken);
    Task<VehicleResult?> GetById(int vehicleId, CancellationToken cancellationToken);
    Task<IReadOnlyList<VehicleResult>> GetAll(CancellationToken cancellationToken);
}