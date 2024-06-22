namespace OwnerModule.Handlers;


[RegisterHandler]
public class LinkRequestHandler(IDataService data) : IRequestHandler<LinkRequest>
{
    public async Task Handle(LinkRequest request, CancellationToken cancellationToken)
    {
        if (request.Link)
        {
            await data.Add(request.VehicleId, request.PersonId, cancellationToken);
        }
        else
        {
            await data.Remove(request.VehicleId, request.PersonId, cancellationToken);
        }
    }
}