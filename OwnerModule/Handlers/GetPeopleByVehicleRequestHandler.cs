using PeopleModule.Contracts;

namespace OwnerModule.Handlers;


[RegisterHandler]
public class GetPeopleByVehicleRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetPeopleByVehicleRequest, IReadOnlyList<GetPeopleByVehicleResult>>
{
    [Cache(AbsoluteExpirationSeconds = 60)]
    public async Task<IReadOnlyList<GetPeopleByVehicleResult>> Handle(GetPeopleByVehicleRequest request, CancellationToken cancellationToken)
    {
        var peopleIds = await data.GetPeopleIdsByVehicleId(request.VehicleId, cancellationToken);
        if (peopleIds.Length == 0)
            return Array.Empty<GetPeopleByVehicleResult>();
        
        var results = await mediator.Request(new GetListRequest(peopleIds), cancellationToken);
        return results
            .Select(static x => new GetPeopleByVehicleResult(x.Id, x.FirstName, x.LastName))
            .ToList();
    }
}