namespace VehicleModule.Handlers;


[SingletonHandler]
public class DeleteVehicleCommandHandler(
    IDataService data, 
    IMediator mediator
) : ICommandHandler<DeleteVehicleCommand>
{
    public async Task Handle(DeleteVehicleCommand command, IMediatorContext context, CancellationToken cancellationToken)
    {
        var vehicle = await data.GetById(command.VehicleId, cancellationToken);
        if (vehicle != null)
        {
            await data.Delete(vehicle.Id, cancellationToken);
            await mediator.Publish(
                new DeleteVehicleEvent(vehicle.Id, vehicle.Manufacturer, vehicle.Model),
                cancellationToken
            );
        }
    }
}