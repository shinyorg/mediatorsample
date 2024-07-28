using PeopleModule.Contracts;

namespace OwnerModule.Handlers;


[SingletonHandler]
public class GetPeopleByVehicleRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetPeopleByVehicleRequest, TimestampedResult<ReadOnlyCollection<GetPeopleByVehicleResult>>>
{
    [Cache(AbsoluteExpirationSeconds = 60)]
    public async Task<TimestampedResult<ReadOnlyCollection<GetPeopleByVehicleResult>>> Handle(GetPeopleByVehicleRequest request, CancellationToken cancellationToken)
    {
        var peopleIds = await data.GetPeopleIdsByVehicleId(request.VehicleId, cancellationToken);
        if (peopleIds.Length == 0)
            return Utils.Timestamped(Array.Empty<GetPeopleByVehicleResult>().AsReadOnly(), null);
        
        var results = await mediator.Request(new GetListRequest(peopleIds), cancellationToken);
        var list = results
            .Select(static x => new GetPeopleByVehicleResult(x.Id, x.FirstName, x.LastName))
            .ToList()
            .AsReadOnly();

        return Utils.Timestamped(list, DateTimeOffset.UtcNow);
    }
}