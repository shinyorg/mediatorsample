namespace OwnerModule.Contracts;

public record GetPeopleByVehicleRequest(int VehicleId) : IRequest<IReadOnlyList<GetPeopleByVehicleResult>>, IRequestKey
{
    public string GetKey() => "GetPeopleByVehicleRequest_" + VehicleId;
};

public record GetPeopleByVehicleResult(int Id, string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}