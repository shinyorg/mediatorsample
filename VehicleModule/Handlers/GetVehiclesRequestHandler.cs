namespace VehicleModule.Handlers;


[RegisterHandler]
public class GetVehiclesRequestHandler(IDataService data) : IRequestHandler<GetListRequest, IReadOnlyList<VehicleResult>>
{
    public Task<IReadOnlyList<VehicleResult>> Handle(GetListRequest request, CancellationToken cancellationToken)
        => data.GetAll();
}