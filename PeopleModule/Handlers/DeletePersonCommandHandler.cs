namespace PeopleModule.Handlers;


[MediatorSingleton]
public class DeletePersonCommandHandler(IDataService data) : ICommandHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand command, IMediatorContext context, CancellationToken cancellationToken)
    {
        var person = await data.GetById(command.PersonId, cancellationToken);
        if (person != null)
        {
            await data.Delete(command.PersonId, cancellationToken);
            await context.Publish(
                new DeletePersonEvent(person.Id, person.FirstName, person.LastName), 
                true,
                cancellationToken
            );
        }
    }
}