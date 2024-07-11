using VehicleModule.Contracts;

namespace OwnerModule.Handlers;


[SingletonHandler]
public class GetVehiclesByPersonRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetVehiclesByPersonRequest, IReadOnlyList<GetVehiclesByPersonResult>>
{
    [Cache(AbsoluteExpirationSeconds = 60)]
    public async Task<IReadOnlyList<GetVehiclesByPersonResult>> Handle(GetVehiclesByPersonRequest request, CancellationToken cancellationToken)
    {
        var vehicleIds = await data.GetVehicleIdsByPersonId(request.PersonId, cancellationToken);
        if (vehicleIds.Length == 0)
            return Array.Empty<GetVehiclesByPersonResult>();
        
        var results = await mediator.Request(new GetListRequest(vehicleIds), cancellationToken);
        return results
            .Select(static x => new GetVehiclesByPersonResult(x.Id, x.Manufacturer, x.Model))
            .ToList();
    }
}