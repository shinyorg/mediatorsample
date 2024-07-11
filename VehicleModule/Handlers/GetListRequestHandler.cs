namespace VehicleModule.Handlers;


[SingletonHandler]
public class GetListRequestHandler(IDataService data) : IRequestHandler<GetListRequest, IReadOnlyList<VehicleResult>>
{
    public async Task<IReadOnlyList<VehicleResult>> Handle(GetListRequest request, CancellationToken cancellationToken)
    {
        var vehicles = await data.GetAll(cancellationToken);
        var ids = request.VehicleIds ?? [];
        
        if (ids.Length > 0)
        {
            // this feature team took the lazy route because they don't feel there is enough data here to warrant
            // another function or filtering at the database level
            vehicles = vehicles
                .Where(x => ids.Any(y => y == x.Id))
                .ToList();
        }
        return vehicles;
    }
}