namespace OwnerModule.Services;

public interface IDataService
{
    Task Add(int vehicleId, int personId, CancellationToken cancellationToken);
    Task Remove(int vehicleId, int personId, CancellationToken cancellationToken);

    Task<int[]> GetPeopleIdsByVehicleId(int vehicleId, CancellationToken cancellationToken);
    Task<int[]> GetVehicleIdsByPersonId(int personId, CancellationToken cancellationToken);

    Task DeleteByVehicle(int vehicleId, CancellationToken cancellationToken);
    Task DeleteByPerson(int personId, CancellationToken cancellationToken);
}