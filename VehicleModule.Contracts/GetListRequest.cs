namespace VehicleModule.Contracts;


public record GetListRequest(int[]? VehicleIds = null) : IRequest<IReadOnlyList<VehicleResult>>;

public record VehicleResult(int Id, string Manufacturer, string Model)
{
    public string Name => $"{Manufacturer} {Model}";
};