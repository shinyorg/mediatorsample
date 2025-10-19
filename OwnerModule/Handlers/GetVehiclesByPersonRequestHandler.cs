using VehicleModule.Contracts;

namespace OwnerModule.Handlers;


[MediatorSingleton]
public partial class GetVehiclesByPersonRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<GetVehiclesByPersonRequest, ReadOnlyCollection<GetVehiclesByPersonResult>>
{
    [Cache(AbsoluteExpirationSeconds = 60)]
    public async Task<ReadOnlyCollection<GetVehiclesByPersonResult>> Handle(GetVehiclesByPersonRequest request, IMediatorContext context, CancellationToken cancellationToken)
    {
        var vehicleIds = await data.GetVehicleIdsByPersonId(request.PersonId, cancellationToken);
        if (vehicleIds.Length == 0)
            return Array.Empty<GetVehiclesByPersonResult>().AsReadOnly();
        
        var results = (await mediator.Request(new GetListRequest(vehicleIds), cancellationToken)).Result;
        var list = results
            .Select(static x => new GetVehiclesByPersonResult(x.Id, x.Manufacturer, x.Model))
            .ToList()
            .AsReadOnly();

        return list;
    }
}