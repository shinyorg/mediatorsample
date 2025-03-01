namespace OwnerModule.Handlers;


[SingletonHandler]
public class LinkCommandHandler(IDataService data, IMediator mediator) : ICommandHandler<LinkCommand>
{
    public async Task Handle(LinkCommand command, MediatorContext context, CancellationToken cancellationToken)
    {
        if (command.Link)
        {
            await data.Add(command.VehicleId!.Value, command.PersonId!.Value, cancellationToken);
        }
        else
        {
            await data.Remove(command.VehicleId!.Value, command.PersonId!.Value, cancellationToken);
        }

        // we'll tell cache to flush here
        await mediator.FlushAllStores(cancellationToken);
    }
}