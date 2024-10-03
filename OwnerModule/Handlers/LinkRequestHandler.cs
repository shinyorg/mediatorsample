namespace OwnerModule.Handlers;


[SingletonHandler]
public class LinkRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<LinkRequest>
{
    public async Task Handle(LinkRequest request, CancellationToken cancellationToken)
    {
        if (request.Link)
        {
            await data.Add(request.VehicleId!.Value, request.PersonId!.Value, cancellationToken);
        }
        else
        {
            await data.Remove(request.VehicleId!.Value, request.PersonId!.Value, cancellationToken);
        }

        // we'll tell cache to flush here
        await mediator.FlushAllStores(cancellationToken);
    }
}