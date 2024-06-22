namespace VehicleModule.Handlers;


[RegisterHandler]
public class GetListRequestHandler(IDataService data) : IRequestHandler<GetListRequest, IReadOnlyList<VehicleResult>>
{
    public Task<IReadOnlyList<VehicleResult>> Handle(GetListRequest request, CancellationToken cancellationToken)
        => data.GetAll();
}