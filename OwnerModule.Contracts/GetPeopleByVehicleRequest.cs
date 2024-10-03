namespace OwnerModule.Contracts;

// reflectkey is used as the IRequestKey default implementation - which looks at all public/instance/getter properties that are not null and builds a key from them
public record GetPeopleByVehicleRequest(int VehicleId) : IRequest<ReadOnlyCollection<GetPeopleByVehicleResult>>, IRequestKey
{
};

public record GetPeopleByVehicleResult(int Id, string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}