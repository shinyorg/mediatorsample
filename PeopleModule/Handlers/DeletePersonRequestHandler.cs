namespace PeopleModule.Handlers;


[SingletonHandler]
public class DeletePersonRequestHandler(IDataService data, IMediator mediator) : IRequestHandler<DeletePersonRequest>
{
    public async Task Handle(DeletePersonRequest request, CancellationToken cancellationToken)
    {
        var person = await data.GetById(request.PersonId, cancellationToken);
        if (person != null)
        {
            await data.Delete(request.PersonId, cancellationToken);
            await mediator.Publish(
                new DeletePersonEvent(person.Id, person.FirstName, person.LastName), 
                cancellationToken
            );
        }
    }
}