namespace OwnerModule.Contracts;

[ContractKey("GetVehiclesByPersonRequest_{PersonId}")]
public partial record GetVehiclesByPersonRequest(int PersonId) : IRequest<ReadOnlyCollection<GetVehiclesByPersonResult>>;

[SourceGenerateJsonConverter]
public partial record GetVehiclesByPersonResult(int Id, string Manufacturer, string Model)
{
    public string FullName => $"{Manufacturer} {Model}";
}