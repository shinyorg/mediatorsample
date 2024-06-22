namespace OwnerModule.Contracts;

public record GetVehiclesByPersonRequest(int PersonId) : IRequest<IReadOnlyList<GetVehiclesByPersonResult>>;
public record GetVehiclesByPersonResult(int Id, string Manufacturer, string Model)
{
    public string FullName => $"{Manufacturer} {Model}";
}