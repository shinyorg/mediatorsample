using System.Collections.ObjectModel;
using VehicleModule.Contracts;

namespace OwnerModule.Handlers;


[SingletonHandler]
public class GetVehiclesByPersonRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetVehiclesByPersonRequest, TimestampedResult<ReadOnlyCollection<GetVehiclesByPersonResult>>>
{
    [Cache(AbsoluteExpirationSeconds = 60)]
    public async Task<TimestampedResult<ReadOnlyCollection<GetVehiclesByPersonResult>>> Handle(GetVehiclesByPersonRequest request, CancellationToken cancellationToken)
    {
        var vehicleIds = await data.GetVehicleIdsByPersonId(request.PersonId, cancellationToken);
        if (vehicleIds.Length == 0)
            return Utils.Timestamped(Array.Empty<GetVehiclesByPersonResult>().AsReadOnly(), DateTimeOffset.UtcNow);
        
        var results = await mediator.Request(new GetListRequest(vehicleIds), cancellationToken);
        var list = results
            .Select(static x => new GetVehiclesByPersonResult(x.Id, x.Manufacturer, x.Model))
            .ToList()
            .AsReadOnly();

        return Utils.Timestamped(list);
    }
}