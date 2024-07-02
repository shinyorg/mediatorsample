using PeopleModule.Contracts;
using Shiny.Mediator.Middleware;
using VehicleModule.Contracts;

namespace OwnerModule.Handlers;


[RegisterHandler]
public class DeleteEventHandlers(IMediator mediator, IDataService data) : IEventHandler<DeleteVehicleEvent>, IEventHandler<DeletePersonEvent>
{
    public async Task Handle(DeleteVehicleEvent @event, CancellationToken cancellationToken)
    {
        await data.DeleteByVehicle(@event.VehicleId, cancellationToken);
        
        // this tells built-in mediator components to bust their cache values
        await mediator.FlushAllStores(cancellationToken); 
    }

    
    public async Task Handle(DeletePersonEvent @event, CancellationToken cancellationToken)
    {
        await data.DeleteByPerson(@event.PersonId, cancellationToken);
        
        // this tells bulti-in mediator components to bust their cache values
        await mediator.FlushAllStores(cancellationToken); 
    }
}