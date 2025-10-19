namespace VehicleModule.Handlers;


[MediatorSingleton]
public class DeleteVehicleCommandHandler(IDataService data) : ICommandHandler<DeleteVehicleCommand>
{
    public async Task Handle(DeleteVehicleCommand command, IMediatorContext context, CancellationToken cancellationToken)
    {
        var vehicle = await data.GetById(command.VehicleId, cancellationToken);
        if (vehicle != null)
        {
            await data.Delete(vehicle.Id, cancellationToken);
            await context.Publish(
                new DeleteVehicleEvent(vehicle.Id, vehicle.Manufacturer, vehicle.Model),
                true,
                cancellationToken
            );
        }
    }
}