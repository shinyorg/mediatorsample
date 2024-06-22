namespace VehicleModule.Handlers;


[RegisterHandler]
public class DeleteVehicleRequestHandler(
    IDataService data, 
    IMediator mediator
) : IRequestHandler<DeleteVehicleRequest>
{
    public async Task Handle(DeleteVehicleRequest request, CancellationToken cancellationToken)
    {
        var vehicle = await data.GetById(request.VehicleId, cancellationToken);
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