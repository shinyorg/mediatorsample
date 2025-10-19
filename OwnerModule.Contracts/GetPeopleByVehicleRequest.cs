namespace OwnerModule.Contracts;

[ContractKey("GetPeopleByVehicleRequest_{VehicleId}")]
public partial record GetPeopleByVehicleRequest(int VehicleId) : IRequest<ReadOnlyCollection<GetPeopleByVehicleResult>>;

public record GetPeopleByVehicleResult(int Id, string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}