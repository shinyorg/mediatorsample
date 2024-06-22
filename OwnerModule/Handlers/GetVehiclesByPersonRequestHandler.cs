using VehicleModule.Contracts;

namespace OwnerModule.Handlers;


[RegisterHandler]
public class GetVehiclesByPersonRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetVehiclesByPersonRequest, IReadOnlyList<GetVehiclesByPersonResult>>
{
    // TODO: we can cache this for efficiency
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