namespace OwnerModule.Contracts;

// reflectkey is used as the IRequestKey default implementation - which looks at all public/instance/getter properties that are not null and builds a key from them
public record GetVehiclesByPersonRequest(int PersonId) : IRequest<TimestampedResult<ReadOnlyCollection<GetVehiclesByPersonResult>>>, IRequestKey
{
}

public record GetVehiclesByPersonResult(int Id, string Manufacturer, string Model)
{
    public string FullName => $"{Manufacturer} {Model}";
}