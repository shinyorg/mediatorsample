using PeopleModule.Contracts;
using VehicleModule.Contracts;

namespace OwnerModule.Handlers;


[RegisterHandler]
public class DeleteEventHandlers(IMediator mediator, IDataService data) : IEventHandler<DeleteVehicleEvent>, IEventHandler<DeletePersonEvent>
{
    // TODO: send events to flush cache for these ids
    // TODO: ICacheKey needs to be separated from non-contracts library
    
    public async Task Handle(DeleteVehicleEvent @event, CancellationToken cancellationToken)
    {
        await data.DeleteByVehicle(@event.VehicleId, cancellationToken);
    }

    
    public async Task Handle(DeletePersonEvent @event, CancellationToken cancellationToken)
    {
        await data.DeleteByPerson(@event.PersonId, cancellationToken);
    }
}