namespace PeopleModule.Handlers;


[SingletonHandler]
public class DeletePersonCommandHandler(IDataService data, IMediator mediator) : ICommandHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand command, MediatorContext context, CancellationToken cancellationToken)
    {
        var person = await data.GetById(command.PersonId, cancellationToken);
        if (person != null)
        {
            await data.Delete(command.PersonId, cancellationToken);
            await mediator.Publish(
                new DeletePersonEvent(person.Id, person.FirstName, person.LastName), 
                cancellationToken
            );
        }
    }
}