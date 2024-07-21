using PeopleModule.Contracts;

namespace OwnerModule.Handlers;


[SingletonHandler]
public class GetPeopleByVehicleRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetPeopleByVehicleRequest, TimestampedResult<IReadOnlyList<GetPeopleByVehicleResult>>>
{
    [Cache(AbsoluteExpirationSeconds = 60)]
    public async Task<TimestampedResult<IReadOnlyList<GetPeopleByVehicleResult>>> Handle(GetPeopleByVehicleRequest request, CancellationToken cancellationToken)
    {
        var peopleIds = await data.GetPeopleIdsByVehicleId(request.VehicleId, cancellationToken);
        if (peopleIds.Length == 0)
            return new TimestampedResult<IReadOnlyList<GetPeopleByVehicleResult>>(DateTimeOffset.UtcNow, Array.Empty<GetPeopleByVehicleResult>());
        
        var results = await mediator.Request(new GetListRequest(peopleIds), cancellationToken);
        var list = results
            .Select(static x => new GetPeopleByVehicleResult(x.Id, x.FirstName, x.LastName))
            .ToList();

        return new TimestampedResult<IReadOnlyList<GetPeopleByVehicleResult>>(DateTimeOffset.UtcNow, list);
    }
}