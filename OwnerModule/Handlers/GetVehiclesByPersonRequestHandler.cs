using VehicleModule.Contracts;

namespace OwnerModule.Handlers;


[SingletonHandler]
public class GetVehiclesByPersonRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetVehiclesByPersonRequest, TimestampedResult<IReadOnlyList<GetVehiclesByPersonResult>>>
{
    [Cache(AbsoluteExpirationSeconds = 60)]
    public async Task<TimestampedResult<IReadOnlyList<GetVehiclesByPersonResult>>> Handle(GetVehiclesByPersonRequest request, CancellationToken cancellationToken)
    {
        var vehicleIds = await data.GetVehicleIdsByPersonId(request.PersonId, cancellationToken);
        if (vehicleIds.Length == 0)
            return new TimestampedResult<IReadOnlyList<GetVehiclesByPersonResult>>(DateTimeOffset.UtcNow, Array.Empty<GetVehiclesByPersonResult>());
        
        var results = await mediator.Request(new GetListRequest(vehicleIds), cancellationToken);
        var list = results
            .Select(static x => new GetVehiclesByPersonResult(x.Id, x.Manufacturer, x.Model))
            .ToList();

        return new TimestampedResult<IReadOnlyList<GetVehiclesByPersonResult>>(DateTimeOffset.UtcNow, list);
    }
}