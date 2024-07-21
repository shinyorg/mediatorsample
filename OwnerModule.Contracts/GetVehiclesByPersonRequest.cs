namespace OwnerModule.Contracts;

public record GetVehiclesByPersonRequest(int PersonId) : IRequest<TimestampedResult<IReadOnlyList<GetVehiclesByPersonResult>>>, IRequestKey
{
    public string GetKey() => "GetVehiclesByPersonRequest_" + PersonId;
    // public string GetKey() => this.ReflectKey();
}

public record GetVehiclesByPersonResult(int Id, string Manufacturer, string Model)
{
    public string FullName => $"{Manufacturer} {Model}";
}