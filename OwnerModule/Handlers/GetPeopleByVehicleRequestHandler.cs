using PeopleModule.Contracts;

namespace OwnerModule.Handlers;


[SingletonHandler]
public class GetPeopleByVehicleRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetPeopleByVehicleRequest, ReadOnlyCollection<GetPeopleByVehicleResult>>
{
    [Cache(AbsoluteExpirationSeconds = 60)]
    public async Task<ReadOnlyCollection<GetPeopleByVehicleResult>> Handle(GetPeopleByVehicleRequest request, IMediatorContext context, CancellationToken cancellationToken)
    {
        var peopleIds = await data.GetPeopleIdsByVehicleId(request.VehicleId, cancellationToken);
        if (peopleIds.Length == 0)
            return Array.Empty<GetPeopleByVehicleResult>().AsReadOnly();
        
        var results = await mediator.Request(new GetListRequest(peopleIds), cancellationToken);
        var list = results
            .Select(static x => new GetPeopleByVehicleResult(x.Id, x.FirstName, x.LastName))
            .ToList()
            .AsReadOnly();

        return list;
    }
}