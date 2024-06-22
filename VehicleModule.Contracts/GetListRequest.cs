namespace VehicleModule.Contracts;


public record GetListRequest : IRequest<IReadOnlyList<VehicleResult>>;

public record VehicleResult(int Id, string Manufacturer, string Model)
{
    public string Name => $"{Manufacturer} {Model}";
};